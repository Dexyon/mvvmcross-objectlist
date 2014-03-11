using System;
using System.Collections.Generic;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Dexyon.MvvmCrossObjectList.Proxy;

namespace MvvmCrossObjectList.Droid.UILib
{
	public class ObjectListAdapter : MvxAdapter
	{
		public ObjectListAdapter (Android.Content.Context context, IMvxAndroidBindingContext bindingContext)
			: base(context, bindingContext)
		{
		}

		private bool _setup = true;
		private List<TemplateSelector> _templateSelectors = null;

		public void Setup(List<TemplateSelector> templateSelectors)
		{
			_templateSelectors = templateSelectors;
			if (_templateSelectors != null)
				_setup = false;
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
				if (_setup)
					return 1;

				return _templateSelectors.Count;
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
	}

	public class TemplateSelector
	{
		public TemplateSelector(Predicate<ProxyProperty> condition) 
			: this ( condition, Resource.Layout.ListItem_ReadOnly)
		{ }

		public TemplateSelector(Predicate<ProxyProperty> condition, int templateId)
		{
			Condition = condition;
			TemplateId = templateId;
		}

		public Predicate<ProxyProperty> Condition {get;set;}
		public int TemplateId {get;set;}
	}
}

