using System.Collections.Generic;
using System.Linq;

namespace ConferenceManager.Database.Model
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;
	public class Speaker : IEntity
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
		public string EMail { get; set; }

		public byte[] Image { get; set; }

		public string Description { get; set; }

		public int TimeSlotId { get; set; }
		public TimeSlot TimeSlot { get; set; }

		public void Delete()
		{
			using (var context = CMContext.NewContext())
			{
				TimeSlot.Delete(this.TimeSlots);
				context.Speakers.Remove(this);
				context.SaveChanges();
			}
		}

		public static void Delete(ICollection<Speaker> speakers)
		{
			if (speakers.Any())
			{
				foreach (Speaker speaker in speakers)
				{
					speaker.Delete();
				}
			}
		}
	}
}
