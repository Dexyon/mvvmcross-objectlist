using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using Dexyon.MvvmCrossObjectList.Core;

namespace Dexyon.MvvmCrossObjectList.Touch {
	public class Setup : MvxTouchSetup {
		public Setup(MvxApplicationDelegate applicationDelegate, IMvxTouchViewPresenter presenter)
			: base(applicationDelegate, presenter)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			var app = new App();
			return app;
		}
	}
}

