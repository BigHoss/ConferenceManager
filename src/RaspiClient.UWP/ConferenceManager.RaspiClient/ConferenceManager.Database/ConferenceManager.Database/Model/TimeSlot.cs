using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Database.Model
{
	public class TimeSlot : IEntity
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

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}
