using System;

namespace Dexyon.MvvmCrossObjectList.Core.Models
{
	public class Person
	{

		public string FullName { get; set; }

		public string BirthPlace { get; set; }

		public DateTime BirthDate {get;set;}

		//public bool HasChildren { get; set;}

		public int Age
		{
			get{ 
				return (int)((DateTime.Now - BirthDate).TotalDays / 365);
			}
		}

	}
}

