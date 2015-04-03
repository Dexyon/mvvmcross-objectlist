using System;
using Dexyon.MvvmCrossObjectList.Core.Extensions;
using Dexyon.MvvmCrossObjectList.Core.ViewModels;
using Dexyon.MvvmCrossObjectList.Proxy;

namespace Dexyon.MvvmCrossObjectList.Core.ViewModels {

	/// <summary>
	/// The ViewModel for the SecondExampleOverviewViewModel view
	/// </summary>
	public class SecondExampleOverviewViewModel : BaseBabelViewModel {

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


	}


}
