using System;
using System.Collections.Generic;
using Dexyon.MvvmCrossObjectList.Proxy;
using Cirrious.CrossCore.Converters;
using Cirrious.MvvmCross.Binding.Touch.Views;
using UIKit;
using Foundation;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.Bindings;

namespace MvvmCrossObjectList.Touch.UILib {
	public class ObjectListViewSource : MvxTableViewSource {

		private bool _noCustomTemplates = true;

		private List<TemplateSelector> _templateSelectors = null;

		private static readonly NSString DefaultCellIdentifier = 
			new NSString("MvxDefaultObjectListTableCell");
		
		private readonly IMvxBindingDescriptionParser _bindingParser;

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="MvvmCrossObjectList.Touch.UILib.ObjectListViewSource"/> class.
		/// </summary>
		/// <param name="pointer">Pointer.</param>
		public ObjectListViewSource ( IntPtr pointer ) 
			: base ( pointer ) {
			this._bindingParser = Mvx.Resolve<IMvxBindingDescriptionParser>();
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
			this._bindingParser = Mvx.Resolve<IMvxBindingDescriptionParser>();
		}

		public void Setup(List<TemplateSelector> templateSelectors)
		{
			_templateSelectors = templateSelectors;
			_noCustomTemplates &= _templateSelectors == null;
		}

		#region implemented abstract members of MvxBaseTableViewSource

		protected override UITableViewCell GetOrCreateCellFor ( UITableView tableView, NSIndexPath indexPath, object item ) {

			NSString cellIdentifier = DefaultCellIdentifier;
			TemplateSelector templateSelector = null;

			if ( !_noCustomTemplates ) {
				foreach ( var sel in _templateSelectors ) {
					if ( sel.Condition ( item as ProxyProperty ) ) {
						cellIdentifier = sel.CellIdentifier;
						templateSelector = sel;
						break;
					}
				}
			}

			var reusableCell = 
				TableView.DequeueReusableCell ( cellIdentifier, indexPath );

			if ( templateSelector == null ) {
				return reusableCell;
			}


			// Check if we can pass a value converter to our custom ListTableViewCell
			// NOTE: This will be deprecated if the text binding works.
			var mvxObjectListTableViewCell = reusableCell as MvxObjectListTableViewCell;
			if ( mvxObjectListTableViewCell != null ) {
				mvxObjectListTableViewCell.ValueConverter = templateSelector.ValueConverter;
			}

			// Check if we need to set a custom binding
			var mvxBindingContextOwner = reusableCell as IMvxBindingContextOwner;
			if ( mvxBindingContextOwner != null && _bindingParser != null ) {
				IEnumerable<MvxBindingDescription> bindingDescriptors = _bindingParser.Parse (templateSelector.BindingText);
				
				mvxBindingContextOwner.AddBindings ( this, bindingDescriptors );
			}

			return reusableCell;
		}

		#endregion
	}

	public class TemplateSelector
	{
		public TemplateSelector( Predicate<ProxyProperty> condition ) 
			: this ( condition, new NSString("MvxDefaultObjectListTableCell") ) 
		{ }

		public TemplateSelector( Predicate<ProxyProperty> condition, string bindingText ) 
			: this ( condition, new NSString("MvxDefaultObjectListTableCell"), bindingText ) 
		{ }

		public TemplateSelector( Predicate<ProxyProperty> condition, NSString cellIdentifier )
		{
			Condition = condition;
			CellIdentifier = cellIdentifier;
		}

		public TemplateSelector( Predicate<ProxyProperty> condition, NSString cellIdentifier, string bindingText )
		{
			Condition = condition;
			CellIdentifier = cellIdentifier;
			BindingText = bindingText;
		}

		public TemplateSelector ( Predicate<ProxyProperty> condition, IMvxValueConverter valueConverter ) 
			: this ( condition, valueConverter, new NSString("MvxDefaultObjectListTableCell") )
		{ }

		public TemplateSelector ( 
			Predicate<ProxyProperty> condition, 
			IMvxValueConverter valueConverter,
			NSString cellIdentifier ) 
			: this ( condition, cellIdentifier )
		{
			this.ValueConverter = valueConverter;
		}

		public Predicate<ProxyProperty> Condition {get;set;}
		public NSString CellIdentifier {get;set;}
		public IMvxValueConverter ValueConverter { get; private set; }
		public string BindingText { get; private set; }
	}
}

