using System;
using Cirrious.CrossCore.Converters;

namespace Dexyon.MvvmCrossObjectList.Touch {
	public class SalaryConverter : MvxValueConverter<double, string> {

		protected override string Convert (double value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return string.Format("€ {0}", value);
		}


		protected double ConvertBack (string value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return double.Parse(value.Replace("€", "").Trim());
		}
	}
}

