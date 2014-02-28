mvvmcross-objectlist
====================

A sample 'Proof of Concept' project to create a object to list binding for MvvmCross

Version 0.1
- Added the first demo project for Touch and Droid



Todo Items. 

- 1) Make use of Native converters to map properties back and forward from 
     our (original) viewModel to a 'Value' on our ListViewItemViewModel.
- 2) Update the UI (LIstView) after a value has changed our (original) viewmodel. 
- 3) Add methods to pass multiple ValueConverters to our BindableListView based upon PropertyName. 
     Example code:
     	this.CreateBinding(source)
			.To<ExampleOverviewViewModel>(vm => vm.ExampleViewModel)
			.WithConversion ( 
				KeyValue<string,MvxConverter> ( "ExampleProperty", new ExampleConverter() ),
				KeyValue<string,MvxConverter> ( "AnotherPropery", new AnotherConverter() ) 
			)
			.Apply();
- 4) Create a mapping to pass our converters via axml. 
 
