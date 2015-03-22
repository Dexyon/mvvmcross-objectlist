using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Dexyon.MvvmCrossObjectList.Droid {
	public class BirthDateValueConverter : MvxValueConverter<DateTime, string> {

		private const string DateTimeFormat = "d"; 

		protected override string Convert (DateTime value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return value.ToString(DateTimeFormat);
		}

		protected override DateTime ConvertBack (string value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return DateTime.ParseExact ( 
				value, 
				DateTimeFormat, 
				CultureInfo.InvariantCulture );
		}
	}
}

