using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManager.ViewModels {
	abstract class TreeItemViewModelBase : BindableBase {
		public string Icon { get; set; }
		public string Text { get; set; }

		public virtual IList<TreeItemViewModelBase> Items => null;

		private bool _isExpanded;

		public bool IsExpanded {
			get { return _isExpanded; }
			set { SetProperty(ref _isExpanded, value); }
		}

	}
}
