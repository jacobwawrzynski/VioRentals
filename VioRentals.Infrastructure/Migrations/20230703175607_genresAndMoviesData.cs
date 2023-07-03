using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VioRentals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class genresAndMoviesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Family" },
                    { 4, "Romance" },
                    { 5, "Thriller" },
                    { 6, "Horror" },
                    { 7, "Drama" },
                    { 8, "Sci-Fi" },
                    { 9, "Fantasy" },
                    { 10, "Mystery" },
                    { 11, "Western" },
                    { 12, "Animation" },
                    { 13, "Adventure" },
                    { 14, "Crime" },
                    { 15, "Documentary" },
                    { 16, "History" },
                    { 17, "Music" },
                    { 18, "War" },
                    { 19, "Biography" },
                    { 20, "Musical" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DateAdded", "GenreFK", "Name", "NumberAvailable", "NumberInStock", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(161), 2, "Hangover", (byte)5, (byte)5, new DateTime(2009, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(168), 1, "Die Hard", (byte)5, (byte)5, new DateTime(1988, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(173), 2, "The Terminator", (byte)5, (byte)5, new DateTime(1984, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(177), 12, "Toy Story", (byte)5, (byte)5, new DateTime(1995, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(181), 4, "Titanic", (byte)5, (byte)5, new DateTime(1997, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(185), 5, "The Sixth Sense", (byte)5, (byte)5, new DateTime(1999, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(189), 1, "The Avengers", (byte)5, (byte)5, new DateTime(2012, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(194), 1, "The Dark Knight", (byte)5, (byte)5, new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(198), 2, "The Lion King", (byte)12, (byte)5, new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(202), 8, "Star Wars", (byte)5, (byte)5, new DateTime(1977, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(206), 12, "The Incredibles", (byte)5, (byte)5, new DateTime(2004, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(210), 13, "The Hunger Games", (byte)5, (byte)5, new DateTime(2012, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(215), 13, "The Hobbit: An Unexpected Journey", (byte)5, (byte)5, new DateTime(2012, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(219), 6, "The Godfather", (byte)5, (byte)5, new DateTime(1972, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2023, 7, 3, 19, 56, 7, 484, DateTimeKind.Local).AddTicks(223), 8, "Inception", (byte)5, (byte)5, new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
