using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Plugins.JsonLocalisation;
using Dexyon.MvvmCrossObjectList.Core;

namespace Dexyon.MvvmCrossObjectList.Core.Services
{
	public class TextProviderBuilder
		: MvxTextProviderBuilder
	{
		public TextProviderBuilder()
			: base(Constants.GeneralNamespace, Constants.RootFolderForResources)
		{
		}

		protected override IDictionary<string, string> ResourceFiles
		{
			get
			{
				var dictionary = Assembly
					.CreatableTypes()
					.Where(t => t.Name.EndsWith("ViewModel"))
					.ToDictionary(t => t.Name, t => t.Name);

				return dictionary;
			}
		}

		private Assembly Assembly {
			get {
				#if Legacy
				return this.GetType ().Assembly
				#else
				return this.GetType ().GetTypeInfo ().Assembly;
				#endif
			}
		}
	}
}