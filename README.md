mvvmcross-objectlist
====================

A simple proxy library, which converts an object into a list of key-values.

## Howto use

There are two ways to use the ObjectList. 
* Create a specific model which contains the properties needed in the UI.
* Update a specific ViewModel with Attributes (which can optionally also be added in the specific model).

### Create A proxyModel with a specific model 

```
public ExampleOverviewViewModel () {
	_currentPerson = new Person ()
	{ 
		FullName = "Jelle Damen",
		BirthDate = new System.DateTime(1987,2,10),
		BirthPlace = "Hoofddorp",
		//HasChildren = false,
		Salary = 450.00
	};

	CurrentPerson = 
		new ProxyModel<Person, ExampleOverviewViewModel>(
			_currentPerson, 
			this,
			() => RaisePropertyChanged ( () => CurrentPerson ));
}

private Person _currentPerson;

public ProxyModel<Person, ExampleOverviewViewModel> CurrentPerson
{ 
	get; 
	set; 
}
```

```
public class Person {

	[ProxyModelAttribute(Description = "Full Name:", Order = 1 )]
	[MvxLang(Text="FullName", TextSource="TextSource")]
	public string FullName { get; set; }

	[ProxyModelAttribute(Description = "Birth Place:", Order = 3 )]
	[MvxLang(Text="BirthPlace", TextSource="TextSource")]
	public string BirthPlace { get; set; }

	[ProxyModelAttribute(Description = "Date of Birth:", Order = 4 )]
	[MvxLang(Text="BirthDate", TextSource="TextSource")]
	public DateTime BirthDate {get;set;}

	//public bool HasChildren { get; set;}

	[ProxyModelAttribute(Description = "Age:", Order = 2 )]
	[MvxLang(Text="Age", TextSource="TextSource")]
	public int Age
	{
		get{ 
			return (int)((DateTime.Now - BirthDate).TotalDays / 365);
		}
	}

	[ProxyModelAttribute(Description = "Salary: ")]
	[MvxLang(Text="Salary", TextSource="TextSource")]
	public double Salary { get; set; }

}
```

### Create A proxyModel based upon the existing ViewModel
```
/// <summary>
/// Initializes a new instance of the
/// <see cref="Dexyon.MvvmCrossObjectList.Core.ViewModels.ExampleOverviewViewModel"/> class.
/// </summary>
public SecondExampleOverviewViewModel () {
	FullName = "Dexyon";
	BirthDate = new System.DateTime ( 1984, 11, 27 );
	BirthPlace = "Bergen";
	Salary = 100.00;

	CurrentPerson = new ProxyModel<SecondExampleOverviewViewModel> (
		this,
		() => RaisePropertyChanged ( () => CurrentPerson ) 
	);
}

private string _fullName;
private string _birthPlace;
private DateTime _birthDate;
private double _salary;

[ProxyModelAttribute ( Description = "Full Name:", Order = 1 )]
[MvxLang ( Text = "FullName", TextSource = "TextSource" )]
public string FullName { 
	get { return _fullName; } 
	set { this.SetAndRaisePropertyChanged ( () => FullName, ref _fullName, value ); } 
}

[ProxyModelAttribute ( Description = "Birth Place:", Order = 3 )]
[MvxLang ( Text = "BirthPlace", TextSource = "TextSource" )]
public string BirthPlace { 
	get { return _birthPlace; } 
	set { this.SetAndRaisePropertyChanged ( () => BirthPlace, ref _birthPlace, value ); } 
}

[ProxyModelAttribute ( Description = "Date of Birth:", Order = 4 )]
[MvxLang ( Text = "BirthDate", TextSource = "TextSource" )]
public DateTime BirthDate { 
	get { return _birthDate; } 
	set { this.SetAndRaisePropertyChanged ( () => BirthDate, ref _birthDate, value ); } 
}

//public bool HasChildren { get; set;}

[ProxyModelAttribute ( Description = "Age:", Order = 2 )]
[MvxLang ( Text = "Age", TextSource = "TextSource" )]
public int Age {
	get { 
		return (int)((DateTime.Now - BirthDate).TotalDays / 365);
	}
}

[ProxyModelAttribute ( Description = "Salary: " )]
[MvxLang ( Text = "Salary", TextSource = "TextSource" )]
public double Salary { 
	get { return _salary; } 
	set { this.SetAndRaisePropertyChanged ( () => Salary, ref _salary, value ); }  
}

public ProxyModel<SecondExampleOverviewViewModel> CurrentPerson { 
	get; 
	set; 
}
``` 

## Version 3.5
- Upgraded the version number to match MvvmCross plugins
- Upgraded to the latest version
- Upgraded the iOS parts to Unified API.

## Version 0.2
- Made the default dell in iOS a default value. Overrides can be done via the constructor.
- Added a default listitem to the Droid Project. Overrides can be done via the constructor.
 
## Version 0.1
- Added the first demo project for Touch and Droid
- Added the set-up for the project


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
 
