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
				Birthdate = new System.DateTime(1987,2,10),
				BirthPlace = "Hoofddorp"
			};

			_currentPersonProxy = new ProxyModel<Person> (_currentPerson);
		}


		private Person _currentPerson;
		private ProxyModel<Person> _currentPersonProxy;
		public ProxyModel<Person> CurrentPerson
		{
			get {return _currentPersonProxy;}
			set{ 
				_currentPersonProxy = value;
				RaisePropertyChanged(()=> CurrentPerson);
			}

		}
    }
}
