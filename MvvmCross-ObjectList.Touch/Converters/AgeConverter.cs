using System;
using Cirrious.CrossCore.Converters;

namespace Dexyon.MvvmCrossObjectList.Touch {
	public class AgeConverter : MvxValueConverter<DateTime, string> {
		protected override string Convert (DateTime value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return "Test";
		}

		protected override DateTime ConvertBack (string value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return new DateTime ( 1970, 1, 1 );
		}
	}
}

