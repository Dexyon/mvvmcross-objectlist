using Dexyon.MvvmCrossObjectList.Attributes;
using Dexyon.MvvmCrossObjectList.ViewModels;

namespace Dexyon.MvvmCrossObjectList.Core.ViewModels {

	/// <summary>
	/// Example view model.
	/// </summary>
	public class ExampleViewModel 
		: BindablePropertyListViewModel<ExampleViewModel> {

		private string _count;
		[BindableListItem( 
			Description = "Number of x:"
			/* No converter required, using ToString() */ )]
		public string Count {
			get { return _count; }
			set { 
				if ( value != _count ) {
					_count = value;
					RaisePropertyChanged ( "Count" );
				}
			}
		}

		private float _distance;
		[BindableListItem( 
			Description = "Distance: ", 
			ValueConverter = "DistanceToKm" )]
		public float Distance {
			get { return _distance; }
			set { 
				if ( value != _distance ) {
					_distance = value;
					RaisePropertyChanged ( "Distance" );
				}
			}
		}
	}
}

