using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace Dexyon.MvvmCrossObjectList.Droid {
	[Activity ( Label = "ExampleOverviewView.Droid", MainLauncher = true )]
	public class ExampleOverviewView : MvxActivity {

		protected override void OnViewModelSet()
		{
			// Set our view from the "main" layout resource
			SetContentView ( Resource.Layout.Main );
		}
	}
}


