using Cirrious.CrossCore;
using Cirrious.MvvmCross.Localization;
using Cirrious.MvvmCross.Plugins.JsonLocalisation;
using Cirrious.MvvmCross.ViewModels;
using Dexyon.MvvmCrossObjectList.Core.Services;
using Dexyon.MvvmCrossObjectList.Core.ViewModels;

namespace Dexyon.MvvmCrossObjectList.Core {
	public class App : MvxApplication
	{
		public App()
		{
			RegisterAppStart<SecondExampleOverviewViewModel>();
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

