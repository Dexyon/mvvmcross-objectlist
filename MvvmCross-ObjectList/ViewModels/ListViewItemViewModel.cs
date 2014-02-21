using Cirrious.MvvmCross.ViewModels;

namespace Dexyon.MvvmCrossObjectList.ViewModels {

	/// <summary>
	/// The ListView ViewModel items.
	/// </summary>
	public class ListViewItemViewModel : MvxViewModel {
		private string _description;
		public string Description
		{
			get { return _description; }
			set
			{
				_description = value;
				RaisePropertyChanged("Description");
			}
		}

		private string _value;
		public string Value
		{
			get { return _value; }
			set
			{
				_value = value;
				RaisePropertyChanged("Value");
			}
		}
	}
}

