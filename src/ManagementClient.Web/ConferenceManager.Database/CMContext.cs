using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConferenceManager.Database
{
	using Microsoft.EntityFrameworkCore;
	using Model;

	public class CMContext : DbContext
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public DbSet<Conference> Conferences { get; set; }
		public DbSet<Day> Days { get; set; }
		public DbSet<Speaker> Speakers { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<TimeSlot> TimeSlots { get; set; }

		public CMContext(DbContextOptions<CMContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
		{
			_httpContextAccessor = httpContextAccessor;
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
			var databasePath = Path.Combine(Environment.CurrentDirectory, "");
			optionsBuilder.UseSqlite($"Data Source=conferenceManager.db");

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
			OnBeforeSaveChanges();

			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			OnBeforeSaveChanges();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			OnBeforeSaveChanges();
			return base.SaveChangesAsync(cancellationToken);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
		{
			OnBeforeSaveChanges();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void OnBeforeSaveChanges()
		{
			foreach (EntityEntry entry in ChangeTracker.Entries())
			{
				if (!(entry.Entity is IEntity entity)) continue;
				switch (entry.State)
				{
					case EntityState.Deleted:
						entity.DeleteDateTime = DateTime.UtcNow;
						entity.DeleteUserName = GetCurrentUsername();
						entry.State = EntityState.Modified;
						break;
					case EntityState.Modified:
						entity.UpdateUserName = GetCurrentUsername();
						entity.UpdateDateTime = DateTime.UtcNow;
						entity.Updates++;
						break;
					case EntityState.Added:
						entity.CreateUser = GetCurrentUsername();
						entity.CreateDateTime = DateTime.UtcNow;
						break;
				}
			}
		}

		private string GetCurrentUsername()
		{
			var identityName = _httpContextAccessor.HttpContext.User.Identity.Name;
			if (string.IsNullOrEmpty(identityName))
			{
				identityName = Environment.UserName;
			}
			return identityName;
		}
	}
}
