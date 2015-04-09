using System;
using System.ComponentModel;

namespace Dexyon.MvvmCrossObjectList.Proxy {
	public class ProxyProperty : INotifyPropertyChanged {
		private Action<object> _valueSetter;
		private Func<object> _valueGetter;
		private Action<ProxyProperty> _notifyValueChanged;

		internal ProxyProperty ( Type valueType, string description, string propertyName, Action<object> valueSetter, Func<object> valueGetter, Action<ProxyProperty> notifyValueChanged ) {
			ValueType = valueType;
			Description = description;
			PropertyName = propertyName;
			_valueGetter = valueGetter;
			_valueSetter = valueSetter;

			_notifyValueChanged = notifyValueChanged;
			BindingText = "";
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		public object Value {
			get {
				return _valueGetter != null ? _valueGetter () : null;
			}
			set {
				if ( _valueSetter != null ) {
					_valueSetter ( value );

					MyValueChangedAsWell ();

					if ( _notifyValueChanged != null )
						_notifyValueChanged ( this );
				} 
			}
		}

		internal void MyValueChangedAsWell () {
			if ( PropertyChanged != null ) {
				PropertyChanged ( this, new PropertyChangedEventArgs ( "Value" ) );
			}
		}

		public string PropertyName {
			get;
			private set;
		}

		public string Description {
			get;
			set;
		}

		public bool IsReadOnly {
			get { 
				return _valueSetter == null;
			}
		}

		public Type ValueType {
			get;
			private set;
		}

		public string BindingText {
			get;
			private set;
		}

	}
}

