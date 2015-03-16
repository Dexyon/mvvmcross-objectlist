using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Localization;
using Cirrious.MvvmCross.ViewModels;

namespace Dexyon.MvvmCrossObjectList.Proxy
{
	public class ProxyModel<T, TViewModel> : ObservableCollection<ProxyProperty> 
		where TViewModel : MvxViewModel 
	{

		private const string TextProvider = "TextProvider";
		private T _baseModel;
		private TViewModel _tViewModel;
		Action _notifyChangedDelegate;

		/// <summary>
		/// Gets the base model. This model is used for the get and the set actions performed on the properties.
		/// </summary>
		/// <value>The base model.</value>
		public T BaseModel { 
			get { return _baseModel; } 
		}

		public ProxyModel (T baseModel, TViewModel viewModel, Action raiseThisPropertyChanged = null, bool notifyAllProperties = true)
		{
			NotifyAllProperties = notifyAllProperties;
			_baseModel = baseModel;
			_tViewModel = viewModel;
			_notifyChangedDelegate = raiseThisPropertyChanged;
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

				var proxyModelAttribute = GetTypedAttribute<ProxyModelAttribute> ( item );
				var mvxLangAttribute = GetTypedAttribute<MvxLangAttribute> ( item );

				//Create a new proxy property
				ProxyProperty proxyProp = new ProxyProperty(
					item.PropertyType,											// Value type
					ConvertDescription(											// Converted description
						item, proxyModelAttribute, mvxLangAttribute, _tViewModel
					),				
					item.Name, 													// The original description name
					item.CanWrite ?												// Setter (if exists)
						new Action<object>((v)=> {
							item.SetValue(instance,v,null);
							NotifyChanged();
						})
						: null, 												
					new Func<object>(()=>item.GetValue(instance,null)), 		// Getter
					ExecuteNotifyAllProperties									// Notifies other properties
				);

				// Add it to the list
				Add (proxyProp);
	
			}
		}

		private static TReturnType GetTypedAttribute<TReturnType>( PropertyInfo info ) where TReturnType : class 
		{
			// Get the attributes from our Attribute component
			var attrs = new List<Attribute>( info.GetCustomAttributes ( typeof(TReturnType), false ));
			return attrs.Any () ? (attrs [0]) as TReturnType : null; 
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

		private static string ConvertDescription (PropertyInfo item, ProxyModelAttribute objectProperty, MvxLangAttribute mvxLangAttribute, TViewModel viewModel) 
		{
			if (mvxLangAttribute == null) 
			{
				return ConvertDescription ( item, objectProperty );
			}

			try {
				var provider = mvxLangAttribute.TextSource ?? TextProvider;
				var property = viewModel.GetType ().GetProperty ( provider ).GetValue ( viewModel ) as IMvxLanguageBinder;

				if (property != null) {
					return property.GetText(mvxLangAttribute.Text);
				}
			} 
			catch (Exception GE) {
				// Fallback
				Mvx.Error("Error while execuring MvxLang with error: {0}", GE);
			}

			return ConvertDescription ( item, objectProperty );
		}

		private static string ConvertDescription (PropertyInfo item, ProxyModelAttribute objectProperty) 
		{
			return objectProperty != null && !string.IsNullOrEmpty(objectProperty.Description) 
				? objectProperty.Description 
					: item.Name;
		}

	}
}

