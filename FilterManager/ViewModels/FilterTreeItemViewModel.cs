using FilterManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.ViewModels {
	class FilterTreeItemViewModel : SimpleTreeItemViewModel {
		public MiniFilter Filter { get; }

		public FilterTreeItemViewModel(MiniFilter filter, params TreeItemViewModelBase[] items) : base(items) {
			Filter = filter;
		}

		public override IEnumerable<ItemProperty> GetProperties() {
			yield return new ItemProperty { Name = "Name", Value = Filter.Name };
			yield return new ItemProperty { Name = "Instances", Value = Filter.Instances.ToString() };
			yield return new ItemProperty { Name = "Frame ID", Value = Filter.FrameID.ToString() };
		}
	}
}
