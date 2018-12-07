namespace ConferenceManager.Database.Model
{
	using System;
	public interface IEntity
	{
		int Id { get; set; }

		DateTime CreateDateTime { get; set; }
		string CreateUser { get; set; }
		DateTime UpdateDateTime { get; set; }
		string UpdateUserName { get; set; }

		DateTime DeleteDateTime { get; set; }
		string DeleteUserName { get; set; }
	}
}
