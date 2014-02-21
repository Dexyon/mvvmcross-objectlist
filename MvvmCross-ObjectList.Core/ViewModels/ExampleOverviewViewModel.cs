using Cirrious.MvvmCross.ViewModels;

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
			ExampleViewModel = new ExampleViewModel () {
				Distance =  (float)(102.4),  
				Count = "0/400",
			};
		}

		private ExampleViewModel _exampleViewModel;
		public ExampleViewModel ExampleViewModel
		{
			get { return _exampleViewModel; }
			set { 
				_exampleViewModel = value;
				_exampleViewModel.ConvertObjectIntoList ( _exampleViewModel );
				RaisePropertyChanged("ExampleViewModel"); 
			}
		}
    }
}
