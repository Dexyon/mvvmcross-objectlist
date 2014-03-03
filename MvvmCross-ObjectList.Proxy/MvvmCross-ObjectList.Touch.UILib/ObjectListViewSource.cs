using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Dexyon.MvvmCrossObjectList.Proxy;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MvvmCrossObjectList.Touch.UILib {
	public class ObjectListViewSource : MvxTableViewSource {

		private bool _noCustomTemplates = true;
		private List<TemplateSelector> _templateSelectors = null;
		private static readonly NSString DefaultCellIdentifier = 
			new NSString("MvxDefaultObjectListTableCell");

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="MvvmCrossObjectList.Touch.UILib.ObjectListViewSource"/> class.
		/// </summary>
		/// <param name="pointer">Pointer.</param>
		public ObjectListViewSource ( IntPtr pointer ) 
			: base ( pointer ) {
		}

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="MvvmCrossObjectList.Touch.UILib.ObjectListViewSource"/> class.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		public ObjectListViewSource( UITableView tableView ) 
			: base ( tableView ) {
			tableView.RegisterNibForCellReuse(
				UINib.FromName("MvxDefaultObjectListTableCell", NSBundle.MainBundle),
				DefaultCellIdentifier);
		}

		public void Setup(List<TemplateSelector> templateSelectors)
		{
			_templateSelectors = templateSelectors;
			_noCustomTemplates &= _templateSelectors == null;
		}

		#region implemented abstract members of MvxBaseTableViewSource

		protected override UITableViewCell GetOrCreateCellFor ( UITableView tableView, NSIndexPath indexPath, object item ) {

			NSString cellIdentifier = DefaultCellIdentifier;

			if ( !_noCustomTemplates ) {
				foreach ( var sel in _templateSelectors ) {
					if ( sel.Condition ( item as ProxyProperty ) ) {
						cellIdentifier = sel.CellIdentifier;
						break;
					}
				}
			}

			return TableView.DequeueReusableCell ( cellIdentifier, indexPath );
		}

		#endregion
	}

	public class TemplateSelector
	{
		public TemplateSelector(Predicate<ProxyProperty> condition, NSString cellIdentifier)
		{
			Condition = condition;
			CellIdentifier = cellIdentifier;
		}

		public Predicate<ProxyProperty> Condition {get;set;}
		public NSString CellIdentifier {get;set;}
	}
}

