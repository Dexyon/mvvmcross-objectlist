using Cirrious.MvvmCross.ViewModels;
using Dexyon.MvvmCrossObjectList.Core.Models;
using Dexyon.MvvmCrossObjectList.Proxy;

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
			_currentPerson = new Person ()
			{ 
				FullName = "Jelle Damen",
				BirthDate = new System.DateTime(1987,2,10),
				BirthPlace = "Hoofddorp",
				//HasChildren = false,
				Salary = 450.00
			};

			CurrentPerson = 
				new ProxyModel<Person>(
					_currentPerson, 
					() => RaisePropertyChanged ( () => CurrentPerson ));
		}

		private Person _currentPerson;

		public ProxyModel<Person> CurrentPerson
		{ 
			get; 
			set; 
		}
    }
}
