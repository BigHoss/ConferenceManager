using System;
using ConferenceManager.Database.Model;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var conference = Conference.CreateUpdate("testConference2", new DateTime(2019,02,4), new DateTime(2019,2,9));
			var conferences = Conference.GetAll();
		}
	}
}
