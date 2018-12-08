using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ConferenceManager.Database.Model
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;
	public class Speaker : IEntity
	{
		public string Name { get; set; }
		public string EMail { get; set; }

		public byte[] Image { get; set; }

		public string Description { get; set; }

		public int TimeSlotId { get; set; }
		public ICollection<TimeSlot> TimeSlots { get; set; }

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
