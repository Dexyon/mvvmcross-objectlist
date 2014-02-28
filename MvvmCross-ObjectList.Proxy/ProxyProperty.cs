using System;

namespace Dexyon.MvvmCrossObjectList.Proxy
{
	public class ProxyProperty
	{
		private Action<object> _valueSetter;
		private Func<object> _valueGetter;
		private Type _valueType;

		public ProxyProperty ()
		{
		}

		internal ProxyProperty (Type valueType, string description, Action<object> valueSetter, Func<object> valueGetter)
		{
			_valueType = valueType;
			Description = description;
			_valueGetter = valueGetter;
			_valueSetter = valueSetter;
		}

		public object Value {
			get{
				if (_valueGetter != null) {
					return _valueGetter ();
				} else {
					return null;
				}
			}
			set{
				if (_valueSetter != null) {
					_valueSetter (value);
				} 
			}
		}

		public string Description {
			get;
			set;
		}

		public bool IsReadOnly{
			get{ 
				return _valueSetter == null;
			}
		}

		public Type ValueType{
			get{ 
				return _valueType;
			}
		}
	}
}

