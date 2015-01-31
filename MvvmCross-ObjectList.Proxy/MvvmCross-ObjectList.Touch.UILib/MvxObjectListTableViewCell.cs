using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.CrossCore.Converters;
using Foundation;

namespace MvvmCrossObjectList.Touch.UILib {
	[Register("MvxObjectListTableViewCell")]
	public class MvxObjectListTableViewCell : MvxTableViewCell {

		/// <summary>
		/// Initializes a new instance of the <see cref="MvvmCrossObjectList.Touch.UILib.MvxObjectListTableViewCell"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public MvxObjectListTableViewCell ( IntPtr handle ) 
			: base ( handle ) {
		}

		/// <summary>
		/// Gets or sets the value converter.
		/// </summary>
		/// <value>The value converter.</value>
		public IMvxValueConverter ValueConverter { get; set; }
	}
}

