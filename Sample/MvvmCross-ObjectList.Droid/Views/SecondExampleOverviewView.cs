using System;
using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Views;
using MvvmCrossObjectList.Droid.UILib;

namespace Dexyon.MvvmCrossObjectList.Droid {
	[Activity ( Label = "SecondExampleOverviewView.Droid", MainLauncher = true )]
	public class SecondExampleOverviewView : MvxActivity {

		ObjectListAdapter adapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			adapter = new ObjectListAdapter (this.BaseContext, (IMvxAndroidBindingContext)this.BindingContext);

			//One layout to rule them all
			adapter.Setup (new System.Collections.Generic.List<TemplateSelector> ()
				{ 
					new TemplateSelector(x=> x.PropertyName == "Salary", Resource.Layout.ListItem_Salary),
					new TemplateSelector(c=>c.IsReadOnly, Resource.Layout.listitem_readonly),
					//new TemplateSelector((c)=>c.ValueType == typeof(bool),Resource.Layout.ListItem_Bool),
					new TemplateSelector(c=>c.PropertyName == "Age", Resource.Layout.listitem_readonly),
					new TemplateSelector(c=>c.ValueType == typeof(DateTime),Resource.Layout.ListItem_DatePicker),
					new TemplateSelector(c=>true,Resource.Layout.ListItem_TextEdit),

				}
			);

			// Set our view from the "main" layout resource
			SetContentView ( Resource.Layout.Main );

			FindViewById<MvxListView> ( Resource.Id.PersonList ).Adapter = adapter;
		}
	}
}


