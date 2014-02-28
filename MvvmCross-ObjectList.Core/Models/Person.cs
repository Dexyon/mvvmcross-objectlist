using System;

namespace Dexyon.MvvmCrossObjectList.Core.Models
{
	public class Person
	{

		public string FullName { get; set; }
		public string BirthPlace { get; set;}
		public DateTime Birthdate {get;set;}
		public int Age
		{
			get{ 
				return (int)((DateTime.Now - Birthdate).TotalDays / 365);
			}
		}

	}
}

