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

			// Customer
			modelBuilder.Entity<CustomerEntity>()
				.HasMany(c => c._Rentals)
				.WithOne(r => r._Customer)
				.HasForeignKey(r => r.CustomerFK)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<CustomerEntity>()
				.Property(c => c.MembershipType)
				.HasConversion<string>();

			// Genre
			modelBuilder.Entity<GenreEntity>()
				.HasMany(g => g._Movies)
				.WithOne(m => m._Genre)
				.HasForeignKey(m => m.GenreFK)
				.OnDelete(DeleteBehavior.ClientSetNull);

            // Movie
            modelBuilder.Entity<MovieEntity>()
                .HasMany(m => m._Rentals)
                .WithOne(r => r._Movie)
                .HasForeignKey(r => r.MovieFK)
                .OnDelete(DeleteBehavior.ClientSetNull);

			// Membership
            modelBuilder.Entity<MembershipDetailsEntity>()
				.HasMany(m => m._Customers)
				.WithOne(c => c._MembershipDetails)
				.HasForeignKey(c => c.MembershipDetailsFK)
				.OnDelete(DeleteBehavior.ClientSetNull);

			// DATA FEEDING
			// Membership
			modelBuilder.Entity<MembershipDetailsEntity>()
				.HasData(
					new MembershipDetailsEntity
					{
						Id = 1,
						Name = "PayAsYouGo",
						SignUpFee = 0,
						DurationInMonths = 0,
						DiscountRate = 0
					},
					new MembershipDetailsEntity
					{
						Id = 2,
						Name = "Monthly",
						SignUpFee = 30,
						DurationInMonths = 1,
						DiscountRate = 10
					},
                    new MembershipDetailsEntity
                    {
                        Id = 3,
                        Name = "Quarterly",
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
					new MembershipDetailsEntity
					{
						Id = 4,
						Name = "Annual",
						SignUpFee = 300,
						DurationInMonths = 12,
						DiscountRate = 20
					});

			// Movies
			//modelBuilder.Entity<MovieEntity>()
			//	.HasData
        }
    }
}
