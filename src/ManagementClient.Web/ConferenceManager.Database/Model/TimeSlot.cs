using System.Collections.Generic;
using System.Linq;

namespace ConferenceManager.Database.Model
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;
	public class TimeSlot : IEntity
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

		public int DayId { get; set; }
		public Day Day { get; set; }

		public int RoomId { get; set; }
		public Room Room { get; set; }

		public int SpeakerId { get; set; }
		public Speaker Speaker { get; set; }

		public static TimeSlot CreateUpdate(string name, Day day, Room room, Speaker speaker)
		{
			var timeSlot = GetByName(name);
			if (timeSlot != null)
			{
				timeSlot.Day = day;
				timeSlot.Room = room;
				timeSlot.Speaker = speaker;
			}
			else
			{
				timeSlot = new TimeSlot
				{
					Name = name,
					Day = day,
					Room = room,
					Speaker = speaker,
				};
			}

			return CreateUpdate(timeSlot);
		}

		public static TimeSlot CreateUpdate(TimeSlot timeSlot)
		{
			using (var context = CMContext.NewContext())
			{
				if (context.TimeSlots.Any(x => x.Id == timeSlot.Id))
				{
					context.TimeSlots.Update(timeSlot);
				}
				else
				{
					context.TimeSlots.Add(timeSlot);
				}

				context.SaveChanges();
			}

			return timeSlot;
		}

		public static TimeSlot GetByName(string name)
		{
			using (var context = CMContext.NewContext())
			{
				return context.TimeSlots.FirstOrDefault(x => x.Name.Contains(name));
			}
		}

		public void Delete()
		{
			using (var context = CMContext.NewContext())
			{
				context.Remove(this);
				context.SaveChanges();
			}
		}

		public static void Delete(ICollection<TimeSlot> timeSlots)
		{
			if (!timeSlots.Any()) return;
			foreach (TimeSlot timeSlot in timeSlots)
			{
				timeSlot.Delete();
			}
		}
	}
}
