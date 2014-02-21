using Cirrious.MvvmCross.ViewModels;
using Dexyon.MvvmCrossObjectList.Core.ViewModels;

namespace Dexyon.MvvmCrossObjectList.Core {
	public class App : MvxApplication
	{
		public App()
		{
			RegisterAppStart<ExampleOverviewViewModel>();
		}
	}
}

