using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.Core {
	using System.Runtime.InteropServices;
	using static NativeMethods;

	public unsafe sealed class MiniFilter {
		public  static IReadOnlyList<MiniFilter> EnumFilters() {
			IntPtr hFind;
			int returned;
			var size = sizeof(FILTER_FULL_INFORMATION) + 64;
			var buffer = stackalloc byte[size];

			int hr = FilterFindFirst(FilterInformationClass.FullInformation, new IntPtr(buffer), size, out returned, out hFind);
			if (hr < 0)
				Marshal.ThrowExceptionForHR(hr);

			var filters = new List<MiniFilter>(8);
			do {
				var info = (FILTER_FULL_INFORMATION*)buffer;
				var filter = new MiniFilter(info);
				filters.Add(filter);
			} while (0 == FilterFindNext(hFind, FilterInformationClass.FullInformation, new IntPtr(buffer), size, out returned));

			FilterFindClose(hFind);
			return filters;
		}

		public string Name { get; }
		public int Instances { get; }
		public int FrameID { get; }

		private MiniFilter(FILTER_FULL_INFORMATION* info) {
			Instances = info->NumberOfInstances;
			Name = new string(&info->FilterNameBuffer, 0, info->FilterNameLength / 2);
			FrameID = info->FrameID;		
		}
	}
}
