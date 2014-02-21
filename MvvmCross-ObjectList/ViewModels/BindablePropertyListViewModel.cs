using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.ViewModels;
using Dexyon.MvvmCrossObjectList.Attributes;

namespace Dexyon.MvvmCrossObjectList.ViewModels {

	/// <summary>
	/// Bindable property list view model.
	/// </summary>
	public class BindablePropertyListViewModel<T> : MvxViewModel, IEnumerable<ListViewItemViewModel> {

		private static IMvxValueConverterLookup _valueConverters;

		private readonly IList<ListViewItemViewModel> _bindableList = 
			new List<ListViewItemViewModel>();

		#region IEnumerable implementation

		public IEnumerator<ListViewItemViewModel> GetEnumerator () {
			return _bindableList.GetEnumerator ();
		}

		#endregion

		#region IEnumerable implementation

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return _bindableList.GetEnumerator ();
		}

		#endregion

		/// <summary>
		/// Converts the parent object into list.
		/// </summary>
		/// <returns>The object into list.</returns>
		public IEnumerable<ListViewItemViewModel> ConvertObjectIntoList (T objectInstance) {

			_valueConverters = Mvx.Resolve<IMvxValueConverterLookup> ();

			// Get all properties from our object, which make use of our custom attribute
			IList<PropertyInfo> props = typeof(T).GetProperties().Where(
				prop => Attribute.IsDefined(prop, typeof(BindableListItem))).ToList();

			foreach ( var item in props ) {

				// Get the attributes from our Attribute component
				var attrs = item.GetCustomAttributes ( typeof(BindableListItem), false );

				BindableListItem objectProperty = null;
				if ( attrs.Length > 0 ) {
					objectProperty = (BindableListItem)attrs [0];
				} 

				// Create the custom list view item
				var listView = new ListViewItemViewModel () {
					// Check if we have a description, otherwise use the property name..
					Description = ConvertDescription( item, objectProperty, objectInstance ),

					// We should check if there is a value conversion
					Value = ConvertValue(item, objectProperty, objectInstance),
				};

				int sequenceNumber = objectProperty != null && objectProperty.Order > 0
					? objectProperty.Order 
					: int.MaxValue - (props.Count () - props.IndexOf ( item ));
				_bindableList.Add ( listView );
			}
			return null;
	 	}

		private static string ConvertDescription (PropertyInfo item, BindableListItem objectProperty, T objectInstance) {
			/*if (!string.IsNullOrEmpty ( objectProperty.DescriptionFromSettings )) {

				PropertyInfo info = typeof (ISettings).GetProperty(objectProperty.DescriptionFromSettings);
				return info.GetValue ( objectInstance, null );
			}*/

			return 
				objectProperty != null && !string.IsNullOrEmpty(objectProperty.Description) 
				? objectProperty.Description 
				: item.Name;
		}

		private static string ConvertValue (PropertyInfo item, BindableListItem objectProperty, T objectInstance) {
			if ( _valueConverters != null 
				&& objectProperty != null 
				&& !string.IsNullOrEmpty(objectProperty.ValueConverter)) {

				IMvxValueConverter found = _valueConverters.Find ( objectProperty.ValueConverter );

				if ( found != null ) {
					var converted = found.Convert(
						item.GetValue ( objectInstance, null ),
						typeof(string),
						null,
						null);
					return (string)converted;
				}
			}

			return (string)item.GetValue ( objectInstance, null ).ToString ();
		}
	}
}

