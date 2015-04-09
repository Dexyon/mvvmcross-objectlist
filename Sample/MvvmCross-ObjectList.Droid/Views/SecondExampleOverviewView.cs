using System;
using System.Collections.Generic;
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

		protected override void OnCreate ( Bundle bundle ) {
			base.OnCreate ( bundle );
			adapter = new ObjectListAdapter ( this.BaseContext, (IMvxAndroidBindingContext)this.BindingContext );

			var defaultListTemplate = new ListTemplate (
				Resource.Layout.ListItem_TextEdit, Resource.Id.MainCustom_Description, Resource.Id.MainCustom_Value );
			var dateTimeListTemplate = new ListTemplate ( 
				Resource.Layout.ListItem_DatePicker, Resource.Id.MainCustom_Description, Resource.Id.MainCustom_Value );
			var editListTemplate = new ListTemplate ( 
				Resource.Layout.ListItem_TextEdit, Resource.Id.MainCustom_Description, Resource.Id.MainCustom_Value );

			//One layout to rule them all. Update with default (template and views, and views)
			adapter.Setup (
				new List<TemplateSelector> { 
					new TemplateSelector ( x => x.PropertyName == "Salary", "Text Value, Converter=Salary" ),
					new TemplateSelector ( c => c.IsReadOnly || c.PropertyName == "Age" ),
					new TemplateSelector ( c => c.ValueType == typeof(DateTime), dateTimeListTemplate ),
					new TemplateSelector ( c => true, editListTemplate ),
				}, 
				defaultListTemplate );

			// Set our view from the "main" layout resource
			SetContentView ( Resource.Layout.Main );

			var list = FindViewById<MvxListView> ( Resource.Id.PersonList );
			list.Adapter = adapter;
		}
	}
}


