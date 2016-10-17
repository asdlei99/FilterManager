using System.Collections.Generic;

namespace FilterManager.Core {
	using System;
	using System.Runtime.InteropServices;
	using static NativeMethods;
	public unsafe sealed class MiniFilterInstance {

		public string InstanceName { get; }
		public string VolumeName { get; }
		public string Altitude { get; }
		public string FilterName { get; }

		private MiniFilterInstance(INSTANCE_FULL_INFORMATION* info) {
			InstanceName = new string((char*)((sbyte*)info + info->InstanceNameBufferOffset), 0, info->InstanceNameLength / 2);
			VolumeName = new string((char*)((sbyte*)info + info->VolumeNameBufferOffset), 0, info->VolumeNameLength / 2);
			Altitude = new string((char*)((sbyte*)info + info->AltitudeBufferOffset), 0, info->AltitudeLength / 2);
			FilterName = new string((char*)((sbyte*)info + info->FilterNameBufferOffset), 0, info->FilterNameLength / 2);
		}

		public static IReadOnlyList<MiniFilterInstance> EnumInstances(string volumeName) {
			IntPtr hFind;
			int returned;
			var size = sizeof(INSTANCE_FULL_INFORMATION) + 256;
			var buffer = stackalloc byte[size];

			int hr = FilterVolumeInstanceFindFirst(volumeName, FilterVolumeInstanceInformationClass.FullInformation, new IntPtr(buffer), size, out returned, out hFind);
			if (hr < 0)
				Marshal.ThrowExceptionForHR(hr);

			var instances = new List<MiniFilterInstance>(8);
			do {
				var info = (INSTANCE_FULL_INFORMATION*)buffer;
				var inst = new MiniFilterInstance(info);
				instances.Add(inst);
			} while (0 == FilterVolumeInstanceFindNext(hFind, FilterVolumeInstanceInformationClass.FullInformation, new IntPtr(buffer), size, out returned));

			FilterVolumeInstanceFindClose(hFind);
			return instances;
		}
	}
}