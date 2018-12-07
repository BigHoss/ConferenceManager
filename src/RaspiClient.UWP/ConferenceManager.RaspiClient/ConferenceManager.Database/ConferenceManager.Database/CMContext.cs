namespace ConferenceManager.Database
{
	using Microsoft.EntityFrameworkCore;
	using Model;

	public class CMContext : DbContext
	{
		public DbSet<Conference> Conferences { get; set; }
		public DbSet<Day> Days { get; set; }
		public DbSet<Speaker> Speakers { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<TimeSlot> TimeSlots { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=conferenceManager.db");
		}
	}
}
