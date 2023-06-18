﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VioRentals.Infrastructure.Data;

#nullable disable

namespace VioRentals.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230618131926_modelsRebuild")]
    partial class modelsRebuild
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSubscribingToNewsletter")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MembershipTypeFK")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MembershipTypeFK");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.GenreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.MembershipTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("DiscountRate")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("DurationInMonths")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<short>("SignUpFee")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MembershipTypes");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.MovieEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<int>("GenreFK")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte>("NumberAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("NumberInStock")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GenreFK");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.RentalEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerFK")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateRented")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateReturned")
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieFK")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Returned")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CustomerFK");

                    b.HasIndex("MovieFK");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.CustomerEntity", b =>
                {
                    b.HasOne("VioRentals.Infrastructure.Data.Entities.MembershipTypeEntity", "_MembershipType")
                        .WithMany("_Customers")
                        .HasForeignKey("MembershipTypeFK")
                        .IsRequired();

                    b.Navigation("_MembershipType");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.MovieEntity", b =>
                {
                    b.HasOne("VioRentals.Infrastructure.Data.Entities.GenreEntity", "_Genre")
                        .WithMany("_Movies")
                        .HasForeignKey("GenreFK")
                        .IsRequired();

                    b.Navigation("_Genre");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.RentalEntity", b =>
                {
                    b.HasOne("VioRentals.Infrastructure.Data.Entities.CustomerEntity", "_Customer")
                        .WithMany("_Rentals")
                        .HasForeignKey("CustomerFK")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VioRentals.Infrastructure.Data.Entities.MovieEntity", "_Movie")
                        .WithMany("_Rentals")
                        .HasForeignKey("MovieFK")
                        .IsRequired();

                    b.Navigation("_Customer");

                    b.Navigation("_Movie");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.CustomerEntity", b =>
                {
                    b.Navigation("_Rentals");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.GenreEntity", b =>
                {
                    b.Navigation("_Movies");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.MembershipTypeEntity", b =>
                {
                    b.Navigation("_Customers");
                });

            modelBuilder.Entity("VioRentals.Infrastructure.Data.Entities.MovieEntity", b =>
                {
                    b.Navigation("_Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
