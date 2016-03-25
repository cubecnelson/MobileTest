using System;

namespace Core
{
	public class Meal
	{
		public String Title {get;set;}
		public String Location {get;set;}
		public DateTime CreatedDate {get;set;}
		public byte[] ImageSource {get;set;}

		public Meal ()
		{
			CreatedDate = DateTime.UtcNow;
		}
	}
}

