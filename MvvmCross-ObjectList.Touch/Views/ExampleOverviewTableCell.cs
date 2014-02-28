using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Dexyon.MvvmCrossObjectList.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Dexyon.MvvmCrossObjectList.Touch {
	public partial class ExampleOverviewTableCell : MvxTableViewCell {
		public static readonly UINib Nib = UINib.FromName ( "ExampleOverviewTableCell", NSBundle.MainBundle );
		public static readonly NSString Key = new NSString ( "ExampleOverviewTableCell" );

		public ExampleOverviewTableCell ( IntPtr handle ) : base ( handle ) {
			this.DelayBind(() => {
				var set = 
					this.CreateBindingSet<ExampleOverviewTableCell, ListViewItemViewModel> ();
				set.Bind (lblDescription)
					.To (item => item.Description);
					set.Bind (lblValue)
					.To (item => item.Value);
				set.Apply();
			});
		}

		public static ExampleOverviewTableCell Create () {
			return (ExampleOverviewTableCell)Nib.Instantiate ( null, null ) [0];
		}
	}
}

