using System;
using System.Drawing;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Dexyon.MvvmCrossObjectList.Proxy;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MvvmCrossObjectList.Touch.UILib;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace Dexyon.MvvmCrossObjectList.Touch {
	public partial class SalaryTableCell : MvxTableViewCell {
		public static readonly UINib Nib = UINib.FromName ( "SalaryTableCell", NSBundle.MainBundle );
		public static readonly NSString Key = new NSString ( "SalaryTableCell" );

		public SalaryTableCell ( IntPtr handle ) : base ( handle ) {
			this.DelayBind(() => {
				var bindingSet = 
					this.CreateBindingSet<SalaryTableCell, ProxyProperty> ();

				bindingSet.Bind (lblDescription)
					.To (item => item.Description);

				bindingSet.Bind (lblValue)
					.To (item => item.Value)
					.WithConversion( new SalaryConverter(), null );
				
				bindingSet.Apply();
			});
		}

		public static SalaryTableCell Create () {
			return (SalaryTableCell)Nib.Instantiate ( null, null ) [0];
		}
	}
}

