using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Dtos;
using VioRentals.Models;

namespace VioRentals.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{
		}

		public ApplicationDbContext()
		{
		}

		public DbSet<Customer> Customers { get; set; }
		public virtual DbSet<Movie> Movies { get; set; }
		public DbSet<MembershipType> MembershipTypes { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Rental> Rentals { get; set; }

		public DbSet<MovieDto> MovieDto { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=../VioRentals.db");
			base.OnConfiguring(optionsBuilder);
		}
	}
}
