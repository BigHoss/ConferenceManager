using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManager.Database.Model
{
	using System;
	public interface IEntity
	{
		int Id { get; set; }

		[DisplayName("Time Created")]
		DateTime CreateDateTime { get; set; }
		[DisplayName("User Created")]
		string CreateUser { get; set; }
		[DisplayName("Time Updated")]
		DateTime UpdateDateTime { get; set; }
		[DisplayName("User Updated")]
		string UpdateUserName { get; set; }
		[DisplayName("Times Updated")]
		int Updates { get; set; }
		[DisplayName("Time Deleted")]
		DateTime DeleteDateTime { get; set; }
		[DisplayName("User Deleted")]
		string DeleteUserName { get; set; }

		string Name { get; }

		[NotMapped]
		bool IsDeleted { get; }
	}
}
