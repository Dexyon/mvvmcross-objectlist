using System;

namespace Dexyon.MvvmCrossObjectList.Proxy
{
	public class ProxyProperty
	{
		private Action<object> _valueSetter;
		private Func<object> _valueGetter;

		public ProxyProperty ()
		{
		}

		internal ProxyProperty (string description, Action<object> valueSetter, Func<object> valueGetter)
		{
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


	}
}

