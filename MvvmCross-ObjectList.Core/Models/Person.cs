using System;
using Dexyon.MvvmCrossObjectList.Proxy;

namespace Dexyon.MvvmCrossObjectList.Core.Models
{
	public class Person
	{

		[ProxyModelAttribute(Description = "Full Name:", Order = 1 )]
		public string FullName { get; set; }

		[ProxyModelAttribute(Description = "Birth Place:", Order = 3 )]
		public string BirthPlace { get; set; }

		[ProxyModelAttribute(Description = "Date of Birth:", Order = 4 )]
		public DateTime BirthDate {get;set;}

		//public bool HasChildren { get; set;}

		[ProxyModelAttribute(Description = "Age:", Order = 2 )]
		public int Age
		{
			get{ 
				return (int)((DateTime.Now - BirthDate).TotalDays / 365);
			}
		}

		[ProxyModelAttribute(Description = "Salary: ")]
		public double Salary { get; set; }

	}
}

