// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using System.CodeDom.Compiler;
using Foundation;

namespace MvvmCrossObjectList.Touch.UILib
{
	[Register ("MvxDefaultObjectListTableCell")]
	partial class MvxDefaultObjectListTableCell
	{
		[Outlet]
		UIKit.UILabel lblDescription { get; set; }

		[Outlet]
		UIKit.UILabel lblValue { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblDescription != null) {
				lblDescription.Dispose ();
				lblDescription = null;
			}

			if (lblValue != null) {
				lblValue.Dispose ();
				lblValue = null;
			}
		}
	}
}
