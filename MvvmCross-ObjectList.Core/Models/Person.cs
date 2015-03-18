﻿using System;
using Dexyon.MvvmCrossObjectList.Proxy;

namespace Dexyon.MvvmCrossObjectList.Core.Models
{
	public class Person
	{

		[ProxyModelAttribute(Description = "Full Name:", Order = 1 )]
<<<<<<< HEAD
		[MvxLang(Text="FullName", TextSource="TextSource")]
=======
		[MvxLang(Text="Fullname", TextSource="TextSource")]
>>>>>>> d0ec4927178588b873f41c1a0bfbbbf6291ad7f2
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
}

