using FilterManager.Core;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.ViewModels {
	class MainViewModel : BindableBase {
		ObservableCollection<TreeItemViewModelBase> _items;

		public IList<TreeItemViewModelBase> Items => _items;

		public MainViewModel() {
			_items = new ObservableCollection<TreeItemViewModelBase> {
				new SimpleTreeItemViewModel(RefreshFilters()) { Text = "Filters", Icon = "Filter", IsExpanded = true },
				new SimpleTreeItemViewModel(RefreshVolumes()) { Text = "Volumes", IsExpanded = true }
			}; 

		}

		private TreeItemViewModelBase[] RefreshVolumes() {
			var volumes = Volume.EnumVolumes();
			return volumes.Select(vol => new VolumeTreItemViewModel(vol, RefreshInstances(vol)) { Text = vol.Name }).ToArray();
		}

		private TreeItemViewModelBase[] RefreshInstances(Volume volume) {
			var instances = volume.EnumFilterInstances();
			if (instances == null)
				return null;
			return instances.Select(inst => new InstanceTreeItemViewModel(inst) { Text = inst.InstanceName }).ToArray();
		}

		private TreeItemViewModelBase[] RefreshFilters() {
			var filters = MiniFilter.EnumFilters();
			return filters.Select(filter => new FilterTreeItemViewModel(filter) { Text = filter.Name }).ToArray();
		}

		private SimpleTreeItemViewModel _selectedTreeItem;

		public SimpleTreeItemViewModel SelectedTreeItem {
			get { return _selectedTreeItem; }
			set { if (SetProperty(ref _selectedTreeItem, value)) {
					OnPropertyChanged(nameof(Properties));
				}
			}
		}

		public IEnumerable<ItemProperty> Properties => SelectedTreeItem?.GetProperties();

	}
}
