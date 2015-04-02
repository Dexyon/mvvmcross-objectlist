using System;
using System.Collections.Generic;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Bindings;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Dexyon.MvvmCrossObjectList.Proxy;
using Android.Widget;

namespace MvvmCrossObjectList.Droid.UILib
{
	public class ObjectListAdapter : MvxAdapter
	{

		private readonly IMvxBindingDescriptionParser _bindingParser;

		public ObjectListAdapter (Android.Content.Context context, IMvxAndroidBindingContext bindingContext)
			: base(context, bindingContext)
		{
			this._bindingParser = Mvx.Resolve<IMvxBindingDescriptionParser>();
		}

		private bool _setup = true;
		private List<TemplateSelector> _templateSelectors = null;

		public void Setup(List<TemplateSelector> templateSelectors)
		{
			_templateSelectors = templateSelectors;
			_setup &= _templateSelectors == null;
		}

		public override int GetItemViewType (int position)
		{
			if (_setup)
				return 0;

			var item = GetRawItem (position) as ProxyProperty;

			for (int i = 0; i < _templateSelectors.Count; i++) {
				if (_templateSelectors [i].Condition (item))
					return i;
			} 
			//nothing applies
			return 0;
		}

		public override int ViewTypeCount {
			get { 
				return _setup ? 1 : _templateSelectors.Count;

			}
		}

		protected override View GetBindableView (View convertView, object source, int templateId)
		{
			if (_setup)
				return base.GetBindableView (convertView, source, templateId);

			foreach (var sel in _templateSelectors) {
				if (sel.Condition (source as ProxyProperty)) {
					templateId = sel.TemplateId;
					break;
				}
			}

			return base.GetBindableView (convertView, source, templateId);
		}

		/*protected override IMvxListItemView CreateBindableView (object dataContext, int templateId) {
			var bindable =  base.CreateBindableView (dataContext, templateId);

			TemplateSelector selector = null;

			foreach (var sel in _templateSelectors) {
				if (sel.Condition (source as ProxyProperty)) {
					selector = sel;
					break;
				}
			}

			// Check if we need to set a custom binding
			var mvxBindingContextOwner = viewToUse as IMvxBindingContextOwner;

			if ( mvxBindingContextOwner != null && _bindingParser != null 
				&& selector != null && !string.IsNullOrEmpty(selector.Binding) ) {
				var bindingDescriptors = new List<MvxBindingDescription>( _bindingParser.Parse (selector.Binding));

				mvxBindingContextOwner.AddBindings ( viewToUse, bindingDescriptors );
			}

			return bindable;

		}*/
	}

	public class TemplateSelector
	{
		/*public TemplateSelector(Predicate<ProxyProperty> condition) 
			: this ( condition, Resource.Layout.ListItem_ReadOnly)
		{ }*/

		public TemplateSelector(Predicate<ProxyProperty> condition, int templateId)
		{
			Condition = condition;
			TemplateId = templateId;
		}

		public TemplateSelector(Predicate<ProxyProperty> condition, int templateId, string bindingDescription) 
			: this (condition, templateId)
		{
			Binding = bindingDescription;
		}

		public Predicate<ProxyProperty> Condition {get;set;}
		public int TemplateId {get;set;}
		public string Binding { get; private set;}
	}
}

