using Cirrious.MvvmCross.ViewModels;
using Dexyon.MvvmCrossObjectList.Core.Models;

namespace Dexyon.MvvmCrossObjectList.Core.ViewModels {

    /// <summary>
	/// The ViewModel for the ExampleOverviewViewModel view
    /// </summary>
	public class ExampleOverviewViewModel : MvxViewModel {

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Dexyon.MvvmCrossObjectList.Core.ViewModels.ExampleOverviewViewModel"/> class.
		/// </summary>
		public ExampleOverviewViewModel () {
			ExampleModel = new Person ()
			{ 
				FullName = "Jelle Damen",
				Birthdate = new System.DateTime(1987,2,10),
				BirthPlace = "Hoofddorp"
			};
		}

		private Person _exampleModel;
		public Person ExampleModel
		{
			get { return _exampleModel; }
			set { 
				_exampleModel = value;
				RaisePropertyChanged("ExampleModel"); 
			}
		}
    }
}
