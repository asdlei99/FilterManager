using FilterManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.ViewModels {
	class InstanceTreeItemViewModel : SimpleTreeItemViewModel {
		public MiniFilterInstance Instance { get; }

		public InstanceTreeItemViewModel(MiniFilterInstance instance) {
			Instance = instance;
		}

		public override IEnumerable<ItemProperty> GetProperties() {
			yield return new ItemProperty { Value = Instance.InstanceName, Name = "Instance Name" };
			yield return new ItemProperty { Value = Instance.FilterName, Name = "Filter Name" };
			yield return new ItemProperty { Value = Instance.VolumeName, Name = "Volume Name" };
			yield return new ItemProperty { Value = Instance.Altitude, Name = "Altitude" };
		}
	}
}
