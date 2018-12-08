using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ConferenceManager.Database.Model
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;
	public class TimeSlot : IEntity
	{
		public string Name { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public int DayId { get; set; }
		public Day Day { get; set; }

		public int RoomId { get; set; }
		public Room Room { get; set; }

		public int SpeakerId { get; set; }
		public Speaker Speaker { get; set; }
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
	}
}
