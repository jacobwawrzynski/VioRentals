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
			optionsBuilder.UseSqlite("Data Source=../VioRentalsData.db");
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CustomerEntity>()
				.HasMany(c => c.Rentals)
				.WithOne(r => r._Customer)
				.HasForeignKey(r => r.CustomerFK)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<GenreEntity>()
				.HasMany(g => g.Movies)
				.WithOne(m => m._Genre)
				.HasForeignKey(m => m.GenreFK)
				.OnDelete(DeleteBehavior.ClientSetNull);

			modelBuilder.Entity<MembershipTypeEntity>()
				.HasMany(m => m.Customers)
				.WithOne(c => c._MembershipType)
				.HasForeignKey(c => c.MembershipTypeFK)
				.OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
