using System;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Dexyon.MvvmCrossObjectList.Proxy;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MvvmCrossObjectList.Touch.UILib {
	public partial class MvxDefaultObjectListTableCell : MvxTableViewCell {

		public static readonly UINib Nib = 
			UINib.FromName ( "MvxDefaultObjectListTableCell", NSBundle.MainBundle );
		public static readonly NSString Key = 
			new NSString ( "MvxDefaultObjectListTableCell" );

		public MvxDefaultObjectListTableCell ( IntPtr handle ) : base ( handle ) {
			this.DelayBind(() => {
				var set = 
					this.CreateBindingSet<MvxDefaultObjectListTableCell, ProxyProperty> ();
				set.Bind (lblDescription)
					.To (item => item.Description);
				set.Bind (lblValue)
					.To (item => item.Value);
				set.Apply();
			});

		}

		public static MvxDefaultObjectListTableCell Create () {
			return (MvxDefaultObjectListTableCell)Nib.Instantiate ( null, null ) [0];
		}
	}
}

