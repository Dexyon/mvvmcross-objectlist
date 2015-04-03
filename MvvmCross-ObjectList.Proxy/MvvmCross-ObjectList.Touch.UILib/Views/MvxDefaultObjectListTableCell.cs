using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Dexyon.MvvmCrossObjectList.Proxy;
using Foundation;
using UIKit;

namespace MvvmCrossObjectList.Touch.UILib {
	public partial class MvxDefaultObjectListTableCell : MvxObjectListTableViewCell {

		public static readonly UINib Nib = 
			UINib.FromName ( "MvxDefaultObjectListTableCell", NSBundle.MainBundle );
		public static readonly NSString Key = 
			new NSString ( "MvxDefaultObjectListTableCell" );

		public MvxDefaultObjectListTableCell ( IntPtr handle ) 
			: base ( handle ) {
			this.DelayBind(() => {
				var bindingSet = 
					this.CreateBindingSet<MvxDefaultObjectListTableCell, ProxyProperty> ();
				bindingSet.Bind (lblDescription)
					.To (item => item.Description);

				if ( base.ValueConverter != null ) {
					bindingSet.Bind (lblValue)
						.To (item => item.Value)
						.WithConversion( base.ValueConverter, null );
				} else {
					bindingSet.Bind (lblValue)
						.To (item => item.Value);
				}

				bindingSet.Apply();
			});
		}

		public static MvxDefaultObjectListTableCell Create () {
			return (MvxDefaultObjectListTableCell)Nib.Instantiate ( null, null ) [0];
		}
	}
}

