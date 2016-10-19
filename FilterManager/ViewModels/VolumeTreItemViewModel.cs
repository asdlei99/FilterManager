using FilterManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.ViewModels {
	class VolumeTreItemViewModel : SimpleTreeItemViewModel {
		public Volume Volume { get; }

		public VolumeTreItemViewModel(Volume volume, params TreeItemViewModelBase[] items) : base(items) {
			Volume = volume;
		}

		public override IEnumerable<ItemProperty> GetProperties() {
			yield return new ItemProperty { Name = "Name", Value = Volume.Name };
			yield return new ItemProperty { Name = "File System", Value = Volume.FileSystem.ToString() };
			yield return new ItemProperty { Name = "Frame ID", Value = Volume.FrameID.ToString() };
			yield return new ItemProperty { Name = "Detached?", Value = Volume.IsDetached.ToString() };
		}
	}
}
