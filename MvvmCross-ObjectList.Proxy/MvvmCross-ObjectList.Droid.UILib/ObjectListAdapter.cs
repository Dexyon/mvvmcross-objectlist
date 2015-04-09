using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Dexyon.MvvmCrossObjectList.Proxy;

namespace MvvmCrossObjectList.Droid.UILib
{
	public class ObjectListAdapter : MvxAdapter {

		private const string DEFAULT_TEXT_BINDING = "Text Value";
		private bool _setup = true;
		private List<TemplateSelector> _templateSelectors = null;
		private ListTemplate _defaultListTemplate;

		public ObjectListAdapter ( Android.Content.Context context, IMvxAndroidBindingContext bindingContext )
			: base ( context, bindingContext ) {
		}

		public void Setup ( List<TemplateSelector> templateSelectors, ListTemplate defaultListTemplate ) {
			_templateSelectors = templateSelectors;
			_setup &= _templateSelectors == null;
			_defaultListTemplate = defaultListTemplate;
		}

		public override int GetItemViewType ( int position ) {
			if ( _setup )
				return 0;

			var item = GetRawItem ( position ) as ProxyProperty;

			for ( int i = 0; i < _templateSelectors.Count; i++ ) {
				if ( _templateSelectors [i].Condition ( item ) )
					return i;
			}

			// return the template ID of the default. 
			//nothing applies
			return 0;
		}

		public override int ViewTypeCount {
			get { 
				return _setup ? 1 : _templateSelectors.Count;
			}
		}

		protected override View GetBindableView ( View convertView, object source, int templateId ) {
			if (_setup)
				return base.GetBindableView (convertView, source, templateId);

			ProxyProperty castedProperty = source as ProxyProperty;

			TemplateSelector templateSelector = 
				_templateSelectors.FirstOrDefault ( sel => sel.Condition ( castedProperty ) );

			// Get the list template to use (default or override)
			var listTemplate = GetListTemplate (_defaultListTemplate, templateSelector);

			// Override the template ID. 
			templateId = listTemplate.TemplateId;	

			// Source: http://stackoverflow.com/questions/28985504/mvxadapter-programmatic-binding
			if ( convertView == null ) {
				convertView = base.GetBindableView ( convertView, source, templateId );

				string bindingText = GetBindingText ( templateSelector, castedProperty );

				if ( listTemplate != null && !string.IsNullOrEmpty(bindingText) ) {

					var des = convertView.FindViewById (listTemplate.DescriptionField);
					var tex = convertView.FindViewById (listTemplate.TextField);

					var owner = convertView as IMvxBindingContextOwner;
					owner.CreateBinding ( des ).For ( "Text" ).To ( "Description" ).Apply ();
					owner.AddBindings ( tex, bindingText );
				}
			} else {
				BindBindableView ( source, convertView as IMvxListItemView );
			}

			return convertView;
		}

		/// <summary>
		/// Gets the binding text.
		/// First we focus on the Droid binding (override of the default proxyProperty).
		/// Then we check if we have defined a binding in the proxyProperty.
		/// When we haven't defined any binding's at all we use the default binding.
		/// </summary>
		/// <returns>The binding text.</returns>
		/// <param name="templateSelector">Template selector.</param>
		/// <param name="proxyProperty">Proxy property.</param>
		private static string GetBindingText ( TemplateSelector templateSelector, ProxyProperty proxyProperty ) {
			if ( templateSelector != null && !string.IsNullOrEmpty ( templateSelector.Binding ) ) {
				return templateSelector.Binding;
			} 
			if ( proxyProperty != null && !string.IsNullOrEmpty ( proxyProperty.BindingText ) ) {
				return proxyProperty.BindingText;
			}
			return DEFAULT_TEXT_BINDING;
		}

		/// <summary>
		/// Pick the correct List template (the default given at set-up or an override defined).
		/// </summary>
		/// <returns>The list template.</returns>
		/// <param name="defaultTemplate">Default template.</param>
		/// <param name="templateSelector">Template selector.</param>
		private static ListTemplate GetListTemplate ( ListTemplate defaultTemplate, TemplateSelector templateSelector ) {
			return templateSelector != null && templateSelector.ListTemplate != null 
					? templateSelector.ListTemplate
					: defaultTemplate; 
		}
	}

	public class TemplateSelector {

		public TemplateSelector ( Predicate<ProxyProperty> condition ) {
			Condition = condition;
		}

		public TemplateSelector ( Predicate<ProxyProperty> condition, string bindingDescription ) 
			: this ( condition ) {
			Binding = bindingDescription;
		}

		public TemplateSelector ( Predicate<ProxyProperty> condition, ListTemplate listTemplate ) {
			this.Condition = condition;
			this.ListTemplate = listTemplate;
		}

		public TemplateSelector ( Predicate<ProxyProperty> condition, ListTemplate listTemplate, string bindingDescription )
			: this ( condition, listTemplate ) {
			Binding = bindingDescription;
		}

		/// <summary>
		/// The condition which should be met to use this selector
		/// </summary>
		/// <value>The condition.</value>
		public Predicate<ProxyProperty> Condition { get; private set; }

		/// <summary>
		/// Gets the list template.
		/// </summary>
		/// <value>The list template.</value>
		public ListTemplate ListTemplate { get; private set; }

		/// <summary>
		/// The custom binding
		/// </summary>
		/// <value>The binding.</value>
		public string Binding { get; private set; }
	}

	public class ListTemplate {

		/// <summary>
		/// Initializes a new instance of the <see cref="MvvmCrossObjectList.Droid.UILib.ListTemplate"/> class.
		/// </summary>
		/// <param name="templateId">Template identifier.</param>
		/// <param name="descriptionField">Description field.</param>
		/// <param name="textField">Text field.</param>
		public ListTemplate ( int templateId, int descriptionField, int textField ) {
			this.TemplateId = templateId;
			this.DescriptionField = descriptionField;
			this.TextField = textField;
		}

		/// <summary>
		/// The android template ID
		/// </summary>
		/// <value>The template identifier.</value>
		public int TemplateId { get; private set; }

		/// <summary>
		/// Gets the description view.
		/// </summary>
		/// <value>The description view.</value>
		public int DescriptionField { get; private set; }

		/// <summary>
		/// Gets the text view.
		/// </summary>
		/// <value>The text view.</value>
		public int TextField { get; private set; }
	} 
}

