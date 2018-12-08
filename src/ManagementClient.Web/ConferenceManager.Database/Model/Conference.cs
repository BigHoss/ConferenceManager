using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManager.Database.Model
{
	using System;
	using System.Collections.Generic;
	public class Conference : IEntity
	{
		public string Name { get; set; }
		[DisplayName("Start Time")]
		[DataType(DataType.Date)]
		public DateTime StartTime { get; set; }
		[DisplayName("End Time")]
		[DataType(DataType.Date)]
		public DateTime EndTime { get; set; }

		public ICollection<Day> Days { get; set; }
		public ICollection<Room> Rooms { get; set; }
		public ICollection<Speaker> Speakers { get; set; }

		#region EntityProps
		public int Id { get; set; }
		[DisplayName("Time Created")]
		public DateTime CreateDateTime { get; set; }
		[DisplayName("User Created")]
		public string CreateUser { get; set; }
		[DisplayName("Time Updated")]
		public DateTime UpdateDateTime { get; set; }
		[DisplayName("User Updated")]
		public string UpdateUserName { get; set; }
		[DisplayName("Times Updated")]
		public int Updates { get; set; }
		[DisplayName("Time Deleted")]
		public DateTime DeleteDateTime { get; set; }
		[DisplayName("User Deleted")]
		public string DeleteUserName { get; set; }
		[NotMapped]
		public bool IsDeleted => !string.IsNullOrEmpty(DeleteUserName) && (DeleteDateTime != DateTime.MinValue);
		#endregion


		//public static List<Conference> GetAll(bool withDeleted = false)
		//{
		//	using (var context = CMContext.NewContext())
		//	{
		//		if (withDeleted)
		//		{
		//			return context.Conferences.ToList();
		//		}
		//		else
		//		{
		//			return context.Conferences.Where(x => !x.IsDeleted).ToList();
		//		}
		//	}
		//}

		//public static Conference GetById(int id)
		//{
		//	using (var context = CMContext.NewContext())
		//	{
		//		return context.Conferences.FirstOrDefault(x => x.Id == id);
		//	}
		//}

		//public static Conference GetByName(string name)
		//{
		//	using (var context = CMContext.NewContext())
		//	{
		//		return context.Conferences.FirstOrDefault(x => x.Name == name);
		//	}
		//}

		//public static Conference CreateUpdate(string conferenceName, DateTime startTime, DateTime endTime)
		//{
		//	var conference = GetByName(conferenceName);
		//	if (conference != null)
		//	{
		//		conference.StartTime = startTime;
		//		conference.EndTime = endTime;

		//	}
		//	else
		//	{
		//		conference = new Conference
		//		{
		//			Name = conferenceName,
		//			StartTime = startTime,
		//			EndTime = endTime,
		//		};
		//	}

		//	return CreateUpdate(conference);
		//}

		//public static Conference CreateUpdate(Conference conference)
		//{
		//	using (var context = CMContext.NewContext())
		//	{
		//		if (context.Conferences.Any(x => x.Id == conference.Id))
		//		{
		//			context.Update(conference);
		//		}
		//		else
		//		{
		//			context.Add(conference);
		//		}
		//		context.SaveChanges();
		//		Day.CreateForConference(conference);

		//		return conference;
		//	}
		//}

		//public static void Delete(Conference conference)
		//{
		//	using (var context = CMContext.NewContext())
		//	{
		//		Day.Delete(conference.Days);
		//		Room.Delete(conference.Rooms);
		//		Speaker.Delete(conference.Speakers);

		//		context.Conferences.Remove(conference);
		//		context.SaveChanges();
		//	}
		//}
	}
}
