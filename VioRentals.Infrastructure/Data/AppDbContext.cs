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
					}
                );

            // Genres
            modelBuilder.Entity<GenreEntity>()
                .HasData(
                    new GenreEntity
                    {
                        Id = 1,
                        Name = "Action"
                    },
                    new GenreEntity
                    {
                        Id = 2,
                        Name = "Comedy"
                    },
                    new GenreEntity
                    {
                        Id = 3,
                        Name = "Family"
                    },
                    new GenreEntity
                    {
                        Id = 4,
                        Name = "Romance"
                    },
                    new GenreEntity
                    {
                        Id = 5,
                        Name = "Thriller"
                    },
                    new GenreEntity
                    {
                        Id = 6,
                        Name = "Horror"
                    },
                    new GenreEntity
                    {
                        Id = 7,
                        Name = "Drama"
                    },
                    new GenreEntity
                    {
                        Id = 8,
                        Name = "Sci-Fi"
                    },
                    new GenreEntity
                    {
                        Id = 9,
                        Name = "Fantasy"
                    },
                    new GenreEntity
                    {
                        Id = 10,
                        Name = "Mystery"
                    },
                    new GenreEntity
                    {
                        Id = 11,
                        Name = "Western"
                    },
                    new GenreEntity
                    {
                        Id = 12,
                        Name = "Animation"
                    },
                    new GenreEntity
                    {
                        Id = 13,
                        Name = "Adventure"
                    },
                    new GenreEntity
                    {
                        Id = 14,
                        Name = "Crime"
                    },
                    new GenreEntity
                    {
                        Id = 15,
                        Name = "Documentary"
                    },
                    new GenreEntity
                    {
                        Id = 16,
                        Name = "History"
                    },
                    new GenreEntity
                    {
                        Id = 17,
                        Name = "Music"
                    },
                    new GenreEntity
                    {
                        Id = 18,
                        Name = "War"
                    },
                    new GenreEntity
                    {
                        Id = 19,
                        Name = "Biography"
                    },
                    new GenreEntity
                    {
                        Id = 20,
                        Name = "Musical"
                    }
                );

			// Movies
			modelBuilder.Entity<MovieEntity>()
				.HasData(
					new MovieEntity
					{
						Id = 1,
						Name = "Hangover",
						DateAdded = DateTime.Now,
						NumberInStock = 5,
						ReleaseDate = new DateTime(2009, 06, 05),
						GenreFK = 2,
						NumberAvailable = 5,
					},
                    new MovieEntity
                    {
                        Id = 2,
                        Name = "Die Hard",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(1988, 07, 12),
                        GenreFK = 1,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 3,
                        Name = "The Terminator",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(1984, 10, 26),
                        GenreFK = 2,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 4,
                        Name = "Toy Story",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(1995, 11, 22),
                        GenreFK = 12,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 5,
                        Name = "Titanic",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(1997, 12, 19),
                        GenreFK = 4,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 6,
                        Name = "The Sixth Sense",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(1999, 08, 06),
                        GenreFK = 5,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 7,
                        Name = "The Avengers",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(2012, 04, 25),
                        GenreFK = 1,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 8,
                        Name = "The Dark Knight",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(2008, 07, 18),
                        GenreFK = 1,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 9,
                        Name = "The Lion King",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(1994, 06, 23),
                        GenreFK = 2,
                        NumberAvailable = 12,
                    },
                    new MovieEntity
                    {
                        Id = 10,
                        Name = "Star Wars",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(1977, 05, 25),
                        GenreFK = 8,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 11,
                        Name = "The Incredibles",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(2004, 11, 05),
                        GenreFK = 12,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 12,
                        Name = "The Hunger Games",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(2012, 03, 23),
                        GenreFK = 13,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 13,
                        Name = "The Hobbit: An Unexpected Journey",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(2012, 12, 14),
                        GenreFK = 13,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 14,
                        Name = "The Godfather",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(1972, 03, 15),
                        GenreFK = 6,
                        NumberAvailable = 5,
                    },
                    new MovieEntity
                    {
                        Id = 15,
                        Name = "Inception",
                        DateAdded = DateTime.Now,
                        NumberInStock = 5,
                        ReleaseDate = new DateTime(2010, 07, 16),
                        GenreFK = 8,
                        NumberAvailable = 5,
                    }
                );

		}
    }
}
