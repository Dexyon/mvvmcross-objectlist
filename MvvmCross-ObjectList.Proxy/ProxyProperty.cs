﻿using System;
using System.ComponentModel;

namespace Dexyon.MvvmCrossObjectList.Proxy
{
	public class ProxyProperty : INotifyPropertyChanged
	{
		private Action<object> _valueSetter;
		private Func<object> _valueGetter;
		private Type _valueType;
		private Action<ProxyProperty> _notifyValueChanged;
		private string _tibetBinding;

		public ProxyProperty ()
		{
		}

		internal ProxyProperty (Type valueType, string description, string originalDescription, Action<object> valueSetter, Func<object> valueGetter, Action<ProxyProperty> notifyValueChanged, string tibetBinding)
		{
			_valueType = valueType;
			Description = description;
			OriginalDescription = originalDescription;
			_valueGetter = valueGetter;
			_valueSetter = valueSetter;

			_notifyValueChanged = notifyValueChanged;
			_tibetBinding = tibetBinding;
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

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

					MyValueChangedAsWell();

					if (_notifyValueChanged != null)
						_notifyValueChanged (this);
				} 
			}
		}

		internal void MyValueChangedAsWell()
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs ("Value"));
			}
		}

		public string OriginalDescription {
			get;
			private set;
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

		public string BindingText {
			get {
				return _tibetBinding;
			}
		}
	}
}

