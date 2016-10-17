using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.Core {
	using System.Runtime.InteropServices;
	using static NativeMethods;

	public unsafe sealed class Volume {
		public string Name { get; }
		public int FrameID { get; }
		public bool IsDetached { get; }

		public FileSystemType FileSystem { get; }

		private Volume(FILTER_VOLUME_STANDARD_INFORMATION* info) {
			FrameID = info->FrameID;
			IsDetached = info->Flags != 0;
			Name = new string(&info->FilterVolumeName, 0, info->FilterVolumeNameLength / 2);
			FileSystem = info->FileSystemType;
		}

		public static IReadOnlyList<Volume> EnumVolumes() {
			IntPtr hFind;
			int returned;
			var size = sizeof(FILTER_VOLUME_STANDARD_INFORMATION) + 128;
			var buffer = stackalloc byte[size];

			int hr = FilterVolumeFindFirst(FilterVolumeInformationClass.FullInformation, new IntPtr(buffer), size, out returned, out hFind);
			if (hr < 0)
				Marshal.ThrowExceptionForHR(hr);

			var volumes = new List<Volume>(8);
			do {
				var info = (FILTER_VOLUME_STANDARD_INFORMATION*)buffer;
				var volume = new Volume(info);
				volumes.Add(volume);
			} while (0 == FilterVolumeFindNext(hFind, FilterVolumeInformationClass.FullInformation, new IntPtr(buffer), size, out returned));

			FilterVolumeFindClose(hFind);
			return volumes;
		}

		public IReadOnlyList<MiniFilterInstance> EnumFilterInstances() {
			return MiniFilterInstance.EnumInstances(Name);
		}
	}
}
