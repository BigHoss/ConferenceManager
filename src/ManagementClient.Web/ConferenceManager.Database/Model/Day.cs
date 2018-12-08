using System.Linq;

namespace ConferenceManager.Database.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;
	public class Day : IEntity
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

		public DateTime Date { get; set; }
		[NotMapped]
		public string Name => Date.DayOfWeek.ToString();

		public int ConferenceId { get; set; }
		public Conference Conference { get; set; }

		public ICollection<TimeSlot> TimeSlots { get; set; }

		public static void Create(Conference conference)
		{
			using (var context = CMContext.NewContext())
			{
				if (conference.Days != null)
				{
					foreach (Day conferenceDay in conference.Days)
					{
						if (conferenceDay.TimeSlots.Any())
						{
							foreach (TimeSlot timeSlot in conferenceDay.TimeSlots)
							{
								timeSlot.Delete();
							}
						}

						context.Remove(conferenceDay);
					}
				}

				var days = new List<Day>();
				for (int i = 0; i < (conference.EndTime - conference.StartTime).TotalDays; i++)
				{
					days.Add(new Day
					{
						Conference = conference,
						Date = conference.StartTime.AddDays(i)
					});
				}
				context.Days.AddRange(days);
				context.SaveChanges();
			}
		}

		public void Delete()
		{
			using (var context = CMContext.NewContext())
			{
				TimeSlot.Delete(this.TimeSlots);
				context.Days.Remove(this);
				context.SaveChanges();
			}
		}

		public static void Delete(ICollection<Day> days)
		{
			if (!days.Any()) return;
			foreach (Day day in days)
			{
				day.Delete();
			}
		}
	}
}
