// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Dexyon.MvvmCrossObjectList.Touch
{
	[Register ("ExampleOverviewView")]
	partial class ExampleOverviewView
	{
		[Outlet]
		MonoTouch.UIKit.UILabel BoiletPlateLabel1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel BoiletPlateLabel2 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField BoiletPlateTextField1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField BoiletPlateTextField2 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}

			if (BoiletPlateLabel1 != null) {
				BoiletPlateLabel1.Dispose ();
				BoiletPlateLabel1 = null;
			}

			if (BoiletPlateLabel2 != null) {
				BoiletPlateLabel2.Dispose ();
				BoiletPlateLabel2 = null;
			}

			if (BoiletPlateTextField1 != null) {
				BoiletPlateTextField1.Dispose ();
				BoiletPlateTextField1 = null;
			}

			if (BoiletPlateTextField2 != null) {
				BoiletPlateTextField2.Dispose ();
				BoiletPlateTextField2 = null;
			}
		}
	}
}
