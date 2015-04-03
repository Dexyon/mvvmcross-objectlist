using System;
using System.Linq.Expressions;
using Cirrious.MvvmCross.ViewModels;

namespace Dexyon.MvvmCrossObjectList.Core.Extensions {
	public static class MvxExtensions {
		public static void SetAndRaisePropertyChanged<T> ( this MvxViewModel vm, Expression<Func<T>> property, ref T oldValue, T newValue ) {
			if ( !newValue.Equals ( oldValue ) ) {
				oldValue = newValue;
				vm.RaisePropertyChanged ( property );
			}
		}
	}
}

