using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Database.Model
{
	public class Conference2Day2Room2Speaker2TimeSlot : IEntity
	{
		#region EntityProps
		public int Id { get; set; }
		public DateTime CreateDateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime UpdateDateTime { get; set; }
		public string UpdateUserName { get; set; }
		public DateTime DeleteDateTime { get; set; }
		public string DeleteUserName { get; set; }
		#endregion

		public int ConferenceId { get; set; }
		public Conference Conference { get; set; }

		public int DayId { get; set; }
		public Day Day { get; set; }

		public int RoomId { get; set; }
		public Room Room { get; set; }

		public int SpeakerId { get; set; }
		public Speaker Speaker { get; set; }
	}
}
