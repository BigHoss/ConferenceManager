using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Database.Model
{
	public class Speaker : IEntity
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

		public string Name { get; set; }
		public string Mail { get; set; }

		public byte[] Image { get; set; }

		public string Description { get; set; }

		public int ConferenceId { get; set; }
		public Conference Conference { get; set; }
	}
}
