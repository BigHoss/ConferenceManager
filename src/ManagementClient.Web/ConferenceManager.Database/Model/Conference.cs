namespace ConferenceManager.Database.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	public class Conference : IEntity
	{
		#region EntityProps
		public int Id { get; set; }
		public DateTime CreateDateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime UpdateDateTime { get; set; }
		public string UpdateUserName { get; set; }
		public int Updates { get; set; }
		public DateTime DeleteDateTime { get; set; }
		public string DeleteUserName { get; set; }
		[NotMapped]
		public bool IsDeleted => !string.IsNullOrEmpty(DeleteUserName) && (DeleteDateTime != DateTime.MinValue);
		#endregion

		public string Name { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public ICollection<Day> Days { get; set; }
		public ICollection<Room> Rooms { get; set; }
		public ICollection<Speaker> Speakers { get; set; }

		public static List<Conference> GetAll(bool withDeleted = false)
		{
			using (var context = CMContext.NewContext())
			{
				if (withDeleted)
				{
					return context.Conferences.ToList();
				}
				else
				{
					return context.Conferences.Where(x => !x.IsDeleted).ToList();
				}
			}
		}

		public static Conference GetById(int id)
		{
			using (var context = CMContext.NewContext())
			{
				return context.Conferences.FirstOrDefault(x => x.Id == id);
			}
		}

		public static Conference GetByName(string name)
		{
			using (var context = CMContext.NewContext())
			{
				return context.Conferences.FirstOrDefault(x => x.Name == name);
			}
		}

		public static Conference CreateUpdate(string conferenceName, DateTime startTime, DateTime endTime)
		{
			var conference = GetByName(conferenceName);
			if (conference != null)
			{
				conference.StartTime = startTime;
				conference.EndTime = endTime;

			}
			else
			{
				conference = new Conference
				{
					Name = conferenceName,
					StartTime = startTime,
					EndTime = endTime,
				};
			}

			return CreateUpdate(conference);
		}

		public static Conference CreateUpdate(Conference conference)
		{
			using (var context = CMContext.NewContext())
			{
				if (context.Conferences.Any(x => x.Id == conference.Id))
				{
					context.Update(conference);
				}
				else
				{
					context.Add(conference);
				}
				context.SaveChanges();
				Day.Create(conference);

				return conference;
			}
		}

		public static void Delete(Conference conference)
		{
			using (var context = CMContext.NewContext())
			{
				Day.Delete(conference.Days);
				Room.Delete(conference.Rooms);
				Speaker.Delete(conference.Speakers);

				context.Conferences.Remove(conference);
				context.SaveChanges();
			}
		}
	}
}
