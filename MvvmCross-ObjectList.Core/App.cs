using Cirrious.MvvmCross.ViewModels;
using Dexyon.MvvmCrossObjectList.Core.ViewModels;
using Dexyon.MvvmCrossObjectList.Core.Services;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.JsonLocalisation;
using Cirrious.MvvmCross.Localization;

namespace Dexyon.MvvmCrossObjectList.Core {
	public class App : MvxApplication
	{
		public App()
		{
			RegisterAppStart<ExampleOverviewViewModel>();
			InitializeText ();
		}

		private void InitializeText()
		{
			var builder = new TextProviderBuilder();
			Mvx.RegisterSingleton<IMvxTextProviderBuilder>(builder);
			Mvx.RegisterSingleton<IMvxTextProvider>(builder.TextProvider);
		}
	}
}

