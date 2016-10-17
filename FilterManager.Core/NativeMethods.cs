using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.Core {
	public enum FileSystemType {
		UNKNOWN,
		RAW,
		NTFS,
		FAT,
		CDFS,
		UDFS,
		LANMAN,
		WEBDAV,
		RDPDR,
		NFS,
		MS_NETWARE,
		NETWARE,
		BSUDF,
		MUP,
		RSFX,
		ROXIO_UDF1,
		ROXIO_UDF2,
		ROXIO_UDF3,
		TACIT,
		FS_REC,
		INCD,
		INCD_FAT,
		EXFAT,
		PSFS,
		GPFS,
		NPFS,
		MSFS,
		CSVFS,
		REFS,
		OPENAFS
	}

	[SuppressUnmanagedCodeSecurity]
	unsafe static class NativeMethods {
		[StructLayout(LayoutKind.Sequential)]
		public struct FILTER_FULL_INFORMATION {
			public uint NextEntryOffset;
			public int FrameID;
			public int NumberOfInstances;
			public ushort FilterNameLength;
			public char FilterNameBuffer;
		}

		public enum FilterInformationClass {
			FullInformation
		}

		[DllImport("Fltlib", PreserveSig = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int FilterFindFirst(FilterInformationClass infoClass, IntPtr buffer, int size, out int returned, out IntPtr hFind);

		[DllImport("Fltlib", PreserveSig = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int FilterFindNext(IntPtr hFind, FilterInformationClass infoClass, IntPtr buffer, int size, out int returned);

		[DllImport("Fltlib", PreserveSig = true, ExactSpelling = true)]
		public static extern int FilterFindClose(IntPtr hFind);

		public enum FilterVolumeInformationClass {
			BasicInformation,
			FullInformation
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FILTER_VOLUME_STANDARD_INFORMATION {
			public uint NextEntryOffset;
			public uint Flags;
			public int FrameID;
			public FileSystemType FileSystemType;
			public ushort FilterVolumeNameLength;
			public char FilterVolumeName;
		}

		[DllImport("Fltlib", PreserveSig = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int FilterVolumeFindFirst(FilterVolumeInformationClass infoClass, IntPtr buffer, int size, out int returned, out IntPtr hFind);

		[DllImport("Fltlib", PreserveSig = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int FilterVolumeFindNext(IntPtr hFind, FilterVolumeInformationClass infoClass, IntPtr buffer, int size, out int returned);
		[DllImport("Fltlib", PreserveSig = true, ExactSpelling = true)]
		public static extern int FilterVolumeFindClose(IntPtr hFind);

		public enum FilterVolumeInstanceInformationClass {
			BasicInformation,
			PartialInformation,
			FullInformation
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct INSTANCE_FULL_INFORMATION {
			public uint NextEntryOffset;
			public ushort InstanceNameLength;
			public ushort InstanceNameBufferOffset;
			public ushort AltitudeLength;
			public ushort AltitudeBufferOffset;
			public ushort VolumeNameLength;
			public ushort VolumeNameBufferOffset;
			public ushort FilterNameLength;
			public ushort FilterNameBufferOffset;
		}

		[DllImport("Fltlib", PreserveSig = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int FilterVolumeInstanceFindFirst(string volume, FilterVolumeInstanceInformationClass infoClass, IntPtr buffer, int size, out int returned, out IntPtr hFind);

		[DllImport("Fltlib", PreserveSig = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int FilterVolumeInstanceFindNext(IntPtr hFind, FilterVolumeInstanceInformationClass infoClass, IntPtr buffer, int size, out int returned);
		[DllImport("Fltlib", PreserveSig = true, ExactSpelling = true)]
		public static extern int FilterVolumeInstanceFindClose(IntPtr hFind);

	}
}
