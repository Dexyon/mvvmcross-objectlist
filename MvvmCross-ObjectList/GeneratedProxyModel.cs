using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Dexyon.MvvmCrossObjectList.Proxy
{
	public class GeneratedProxyModel<T> : IEnumerable<object>
	{
		private T _baseModel;
		private List<ProxyProperty> _propertyList;

		public T baseModel { 
			get { 
				return _baseModel;
				} 
		}

		public GeneratedProxyModel (T baseModel)
		{
			_baseModel = baseModel;

			//Create proxy property list
			CreateProxyPropertyList (_baseModel);
		}

		#region IEnumerable implementation
		public IEnumerator<object> GetEnumerator ()
		{
			return _propertyList.GetEnumerator ();
		}
		#endregion
		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return _propertyList.GetEnumerator ();
		}
		#endregion

		private void CreateProxyPropertyList (T instance) {

			//Create a list that holds all proxy properties
			_propertyList = new List<ProxyProperty> ();

			// Get all properties from our object
			IList<PropertyInfo> props = typeof(T).GetProperties().ToList();

			foreach ( var item in props ) {

				//Create a new proxy property
				ProxyProperty proxyProp = new ProxyProperty (
					item.Name, 														// description
					new Action<object>((v)=> item.SetValue(instance,v,null)), // setter
					new Func<object>(()=>item.GetValue(instance,null)) 		// getter
				);

				//Add it to the list
				_propertyList.Add (proxyProp);
	
			}
		}

	}
}

