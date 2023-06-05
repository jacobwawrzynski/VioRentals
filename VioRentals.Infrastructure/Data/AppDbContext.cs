using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data.Entities;

namespace VioRentals.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
		{
		}

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<UserEntity> Users { get; set; }
		public DbSet<CustomerEntity> Customers { get; set; }
		public DbSet<GenreEntity> Genres { get; set; }
		public DbSet<MembershipTypeEntity> MembershipTypes { get; set; }
		public DbSet<MovieEntity> Movies { get; set; }
		public DbSet<RentalEntity> Rentals { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlite("Data Source=VioRentalsData.db");
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
