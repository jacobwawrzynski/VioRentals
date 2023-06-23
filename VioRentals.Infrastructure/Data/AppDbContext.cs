using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

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
		public DbSet<MembershipDetailsEntity> MembershipTypes { get; set; }
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
				.HasMany(c => c._Rentals)
				.WithOne(r => r._Customer)
				.HasForeignKey(r => r.CustomerFK)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<CustomerEntity>()
				.Property(c => c.MembershipType)
				.HasConversion<string>();

			modelBuilder.Entity<GenreEntity>()
				.HasMany(g => g._Movies)
				.WithOne(m => m._Genre)
				.HasForeignKey(m => m.GenreFK)
				.OnDelete(DeleteBehavior.ClientSetNull);

			modelBuilder.Entity<MembershipDetailsEntity>()
				.HasMany(m => m._Customers)
				.WithOne(c => c._MembershipDetails)
				.HasForeignKey(c => c.MembershipDetailsFK)
				.OnDelete(DeleteBehavior.ClientSetNull);

			modelBuilder.Entity<MovieEntity>()
				.HasMany(m => m._Rentals)
				.WithOne(r => r._Movie)
				.HasForeignKey(r => r.MovieFK)
				.OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
