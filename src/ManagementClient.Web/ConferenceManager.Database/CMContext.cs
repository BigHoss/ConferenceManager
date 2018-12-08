using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.IO;

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

		public static CMContext NewContext()
		{
			var context = new CMContext();
			context.Database.Migrate();
			return context;
		}

		static CMContext()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Conference, IEntity>();
				cfg.CreateMap<Day, IEntity>();
				cfg.CreateMap<Speaker, IEntity>();
				cfg.CreateMap<Room, IEntity>();
				cfg.CreateMap<TimeSlot, IEntity>();
			});
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var databasePath = Path.Combine(Environment.CurrentDirectory, "conferenceManager.db");
			optionsBuilder.UseSqlite($"Data Source={databasePath}");

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Conference>().HasQueryFilter(x => !x.IsDeleted);
			modelBuilder.Entity<Day>().HasQueryFilter(x => !x.IsDeleted);
			modelBuilder.Entity<Speaker>().HasQueryFilter(x => !x.IsDeleted);
			modelBuilder.Entity<Room>().HasQueryFilter(x => !x.IsDeleted);
			modelBuilder.Entity<TimeSlot>().HasQueryFilter(x => !x.IsDeleted);
		}

		public override int SaveChanges()
		{
			foreach (EntityEntry entityEntry in ChangeTracker.Entries())
			{
				IEntity entity = Mapper.Map<IEntity>(entityEntry.Entity);

				switch (entityEntry.State)
				{
					case EntityState.Deleted:
						entity.DeleteDateTime = DateTime.UtcNow;
						entity.DeleteUserName = Environment.UserName;
						entityEntry.State = EntityState.Modified;
						break;
					case EntityState.Modified:
						entity.UpdateUserName = Environment.UserName;
						entity.UpdateDateTime = DateTime.UtcNow;
						entity.Updates++;
						break;
					case EntityState.Added:
						entity.CreateUser = Environment.UserName;
						entity.CreateDateTime = DateTime.UtcNow;
						break;
				}
			}

			return base.SaveChanges();
		}
	}
}
