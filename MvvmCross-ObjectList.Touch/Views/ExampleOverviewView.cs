using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Dexyon.MvvmCrossObjectList.Core.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MvvmCrossObjectList.Touch.UILib;

namespace Dexyon.MvvmCrossObjectList.Touch {
	public partial class ExampleOverviewView : MvxViewController {
		public ExampleOverviewView () : base ( "ExampleOverviewView", null ) {
		}

		public new ExampleOverviewViewModel ViewModel {
			get { return (ExampleOverviewViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		public override void DidReceiveMemoryWarning () {
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			/*var source = new MvxStandardTableViewSource(
				TableView,
				UITableViewCellStyle.Subtitle,
				new NSString("ExampleOverviewView"),
				"TitleText Description;DetailText Value",
				UITableViewCellAccessory.DisclosureIndicator);

			this.AddBindings(
				new Dictionary<object, string>()
				{
					{ source, "ItemsSource ExampleViewModel" }
				});

			TableView.Source = source;
			TableView.ReloadData();*/

			TableView.RegisterNibForCellReuse(
				UINib.FromName("SalaryTableCell", NSBundle.MainBundle),
				new NSString("SalaryTableCell")
			);

			var source = new ObjectListViewSource( TableView );
			source.Setup (
				new System.Collections.Generic.List<TemplateSelector> ()
				{ 
					new TemplateSelector ( 
						c => c.ValueType == typeof(DateTime), 
						new BirthDateConverter()
					),
					new TemplateSelector (
						c => c.OriginalDescription == "Salary",
						new NSString("SalaryTableCell")
					),
				}
			);

			TableView.Source = source;

			this.CreateBinding(source)
				.To<ExampleOverviewViewModel>(vm => vm.CurrentPerson)
				.Apply();

			// Perform our MVVM Binding
			var set = this.CreateBindingSet<ExampleOverviewView, ExampleOverviewViewModel> ();

			set.Bind ( source )
				.To ( vm => vm.CurrentPerson );

			set
				.Bind ( BoiletPlateLabel1 )
				.To ( vm => vm.CurrentPerson.baseModel.Age );
			set
				.Bind ( BoiletPlateTextField1 )
				.To ( vm => vm.CurrentPerson.baseModel.Age );

			set
				.Bind ( BoiletPlateLabel2 )
				.To ( vm => vm.CurrentPerson.baseModel.BirthPlace );
			set
				.Bind ( BoiletPlateTextField2 )
				.To ( vm => vm.CurrentPerson.baseModel.BirthPlace );

			set.Apply ();
			TableView.ReloadData ();
		}
	}
}

