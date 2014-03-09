using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Collections.ObjectModel;

namespace Dexyon.MvvmCrossObjectList.Proxy
{
	public class ProxyModel<T> : ObservableCollection<ProxyProperty>
	{
		private T _baseModel;
		Action _notifyChangedDelegate;

		/// <summary>
		/// Gets the base model. This model is used for the get and the set actions performed on the properties.
		/// </summary>
		/// <value>The base model.</value>
		public T baseModel { 
			get { 
				return _baseModel;
				} 
		}

		public ProxyModel (T baseModel, Action RaiseThisPropertyChanged = null, bool notifyAllProperties = true)
		{
			NotifyAllProperties = notifyAllProperties;
			_baseModel = baseModel;
			_notifyChangedDelegate = RaiseThisPropertyChanged;
			//Create proxy property list
			CreateProxyPropertyList (_baseModel);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Dexyon.MvvmCrossObjectList.Proxy.ProxyModel`1"/> notify
		/// all properties when only one single property is changed.
		/// </summary>
		/// <value><c>true</c> if notify all properties; otherwise, <c>false</c>.</value>
		public bool NotifyAllProperties { get; set;}

		private void CreateProxyPropertyList (T instance) {

			// Get all properties from our object
			IList<PropertyInfo> props = typeof(T).GetRuntimeProperties().ToList();

			foreach ( var item in props ) {

				// Get the attributes from our Attribute component
				var attrs = new List<Attribute>(
					item.GetCustomAttributes ( typeof(ProxyModelAttribute), false ));

				ProxyModelAttribute objectProperty = null;
				if ( attrs.Any () ) {
					objectProperty = (ProxyModelAttribute)attrs [0];
				} 

				//Create a new proxy property
				ProxyProperty proxyProp = new ProxyProperty(
					item.PropertyType,											// Value type
					ConvertDescription(item, objectProperty),				 	// converted description
					item.Name, 													// The original description name
					item.CanWrite ?
						new Action<object>((v)=> {
							item.SetValue(instance,v,null);
							NotifyChanged();
						})
						: null, 												// setter (if exists)
					new Func<object>(()=>item.GetValue(instance,null)), 		// getter
					ExecuteNotifyAllProperties									// Notifies other properties
				);

				// Add it to the list
				Add (proxyProp);
	
			}
		}

		private void NotifyChanged()
		{
			if (_notifyChangedDelegate != null)
				_notifyChangedDelegate ();
		}

		private void ExecuteNotifyAllProperties(ProxyProperty sender)
		{
			foreach(var prop in this)
			{
				if (prop != sender) {
					prop.MyValueChangedAsWell ();
				}
			}
		}

		private static string ConvertDescription (PropertyInfo item, ProxyModelAttribute objectProperty) {
			return objectProperty != null && !string.IsNullOrEmpty(objectProperty.Description) 
				? objectProperty.Description 
					: item.Name;
		}

	}
}

