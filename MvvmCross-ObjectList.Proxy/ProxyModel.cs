using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Localization;
using Cirrious.MvvmCross.ViewModels;

namespace Dexyon.MvvmCrossObjectList.Proxy {
	public class ProxyModel<TViewModel> : BaseProxyModel<TViewModel>
		where TViewModel : MvxViewModel {

		/// <summary>
		/// Initializes a new instance of the <see cref="Dexyon.MvvmCrossObjectList.Proxy.ProxyModel`1"/> class.
		/// </summary>
		/// <param name="viewModel">View model.</param>
		/// <param name="raiseThisPropertyChanged">Raise this property changed.</param>
		/// <param name="notifyAllProperties">If set to <c>true</c> notify all properties.</param>
		public ProxyModel ( TViewModel viewModel, Action raiseThisProperyChanged = null )
			: base ( viewModel, PropertyMode.OnlyDecoratedProperties, raiseThisProperyChanged, false ) {

			//Create proxy property list
			CreateProxyPropertyList (
				viewModel, typeof(TViewModel).GetProperties ().Where( 
					prop => prop.GetCustomAttributes(typeof(ProxyModelAttribute), false).Any() ) );
		}
	}
	

	public class ProxyModel<T, TViewModel> : BaseProxyModel<TViewModel> 
		where TViewModel : MvxViewModel {

		/// <summary>
		/// Gets the base model. This model is used for the get and the set actions performed on the properties.
		/// </summary>
		/// <value>The base model.</value>
		public T BaseModel { get; private set; }

		public ProxyModel ( T baseModel, TViewModel viewModel, PropertyMode propertyMode, Action raiseThisPropertyChanged = null, bool notifyAllProperties = true )
			: base ( viewModel, propertyMode, raiseThisPropertyChanged, notifyAllProperties ) {
			BaseModel = baseModel;

			//Create proxy property list
			switch ( propertyMode ) {
			case PropertyMode.OnlyDecoratedProperties:
				CreateProxyPropertyList (
					viewModel, typeof(T).GetProperties ().Where( 
						prop => prop.GetCustomAttributes(typeof(ProxyModelAttribute), false).Any() ) );
				break;
			default:
				CreateProxyPropertyList ( baseModel, typeof(T).GetProperties () );
				break;
			}
		}

		public ProxyModel ( T baseModel, TViewModel viewModel, Action raiseThisPropertyChanged = null, bool notifyAllProperties = true )
			: this ( baseModel, viewModel, PropertyMode.AllPublicProperties, raiseThisPropertyChanged, notifyAllProperties ) {
		}
	}

	public abstract class BaseProxyModel<TViewModel> : ObservableCollection<ProxyProperty>  {

		private const string TextProvider = "TextProvider";
		private readonly Action _notifyChangedDelegate;

		protected TViewModel _viewModel;
		protected PropertyMode _propertyMode;

		/// <summary>
		/// Initializes a new instance of the <see cref="Dexyon.MvvmCrossObjectList.Proxy.BaseProxyModel`1"/> class.
		/// </summary>
		/// <param name="propertyMode">The mode to determin which properties should be read.</param>
		/// <param name="viewModel">The ViewModel on which the proxy property is placed.</param>
		protected BaseProxyModel ( TViewModel viewModel, PropertyMode propertyMode, Action raiseThisPropertyChanged = null, bool notifyAllProperties = true  ) {
			this._viewModel = viewModel;
			this._propertyMode = propertyMode;
			this._notifyChangedDelegate = raiseThisPropertyChanged;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Dexyon.MvvmCrossObjectList.Proxy.ProxyModel`1"/> notify
		/// all properties when only one single property is changed.
		/// </summary>
		/// <value><c>true</c> if notify all properties; otherwise, <c>false</c>.</value>
		public bool NotifyAllProperties { get; set; }

		/// <summary>
		/// Gets the typed attribute from a propert
		/// </summary>
		/// <returns>The typed attribute or NULL if nothing could be found.</returns>
		/// <param name="info">The property from which the attribute should be read.</param>
		/// <typeparam name="TReturnType">The 1st type parameter.</typeparam>
		protected static TReturnType GetTypedAttribute<TReturnType> ( PropertyInfo info ) where TReturnType : class {
			// Get the attributes from our Attribute component
			var attrs = new List<Attribute> ( info.GetCustomAttributes ( typeof(TReturnType), false ) );
			return attrs.Any () ? (attrs [0]) as TReturnType : null; 
		}

		/// <summary>
		/// Converts a description using the MvxLang capabilities.
		/// </summary>
		/// <returns>The translated description.</returns>
		/// <param name="item">The property on which the attributes are set</param>
		/// <param name="objectProperty">The proxy model attribute read from the item</param>
		/// <param name="mvxLangAttribute">The mvxlang attribute read from the item</param>
		/// <param name="viewModel">The view model from which the property was read.</param>
		protected static string ConvertDescription ( PropertyInfo item, ProxyModelAttribute objectProperty, MvxLangAttribute mvxLangAttribute, TViewModel viewModel ) {
			if ( mvxLangAttribute == null ) {
				return ConvertDescription ( item, objectProperty );
			}

			try {
				var provider = mvxLangAttribute.TextSource ?? TextProvider;
				var property = viewModel.GetType ().GetProperty ( provider ).GetValue ( viewModel ) as IMvxLanguageBinder;

				if ( property != null ) {
					return property.GetText ( mvxLangAttribute.Text );
				}
			} catch ( Exception GE ) {
				// Fallback
				Mvx.Error ( "Error while execuring MvxLang with error: {0}", GE );
			}

			return ConvertDescription ( item, objectProperty );
		}

		private static string ConvertDescription ( PropertyInfo item, ProxyModelAttribute objectProperty ) {
			return objectProperty != null && !string.IsNullOrEmpty ( objectProperty.Description ) 
				? objectProperty.Description 
					: item.Name;
		}

		protected void CreateProxyPropertyList<TObject> ( TObject instance, IEnumerable<PropertyInfo> properties ) {
			foreach ( var item in properties ) {

				var proxyModelAttribute = GetTypedAttribute<ProxyModelAttribute> ( item );
				var mvxLangAttribute = GetTypedAttribute<MvxLangAttribute> ( item );

				//Create a new proxy property
				ProxyProperty proxyProp = new ProxyProperty(
					item.PropertyType,											// Value type
					ConvertDescription(											// Converted description
						item, proxyModelAttribute, mvxLangAttribute,_viewModel
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

		private void NotifyChanged () {
			if ( _notifyChangedDelegate != null )
				_notifyChangedDelegate ();
		}

		private void ExecuteNotifyAllProperties ( ProxyProperty sender ) {
			if ( !NotifyAllProperties ) {
				return;
			}

			foreach ( var prop in this ) {
				if ( prop != sender ) {
					prop.MyValueChangedAsWell ();
				}
			}
		}
	}
}

