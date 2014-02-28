using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Dexyon.MvvmCrossObjectList.Proxy
{
	public class ProxyModel<T> : List<ProxyProperty>
	{
		private T _baseModel;

		public T baseModel { 
			get { 
				return _baseModel;
				} 
		}

		public ProxyModel (T baseModel)
		{
			_baseModel = baseModel;

			//Create proxy property list
			CreateProxyPropertyList (_baseModel);
		}

		private void CreateProxyPropertyList (T instance) {

			// Get all properties from our object
			IList<PropertyInfo> props = typeof(T).GetProperties().ToList();

			foreach ( var item in props ) {

				//Create a new proxy property
				ProxyProperty proxyProp = new ProxyProperty(
					item.PropertyType,											// Value type
					item.Name, 												 	// description
					item.CanWrite ?
						new Action<object>((v)=> item.SetValue(instance,v,null))
					: null, 													// setter (if exists)
					new Func<object>(()=>item.GetValue(instance,null)) 			// getter
				);

				//Add it to the list
				Add (proxyProp);
	
			}
		}

	}
}

