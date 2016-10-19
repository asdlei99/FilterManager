using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.ViewModels {
	class SimpleTreeItemViewModel : TreeItemViewModelBase {
		ObservableCollection<TreeItemViewModelBase> _items;

		public SimpleTreeItemViewModel(params TreeItemViewModelBase[] items) {
			if (items != null && items.Length > 0)
				_items = new ObservableCollection<TreeItemViewModelBase>(items);
		}

		public override IList<TreeItemViewModelBase> Items {
			get {
				if (_items == null)
					_items = new ObservableCollection<TreeItemViewModelBase>();
				return _items;
			}
		}

		public virtual IEnumerable<ItemProperty> GetProperties() => null;

	}
}
