using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VioRentals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genre_GenreId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Customer_CustomerId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Movie_MovieId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Rentals",
                newName: "MovieFK");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Rentals",
                newName: "CustomerFK");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_MovieId",
                table: "Rentals",
                newName: "IX_Rentals_MovieFK");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_CustomerId",
                table: "Rentals",
                newName: "IX_Rentals_CustomerFK");

            migrationBuilder.RenameColumn(
                name: "MembershipTypeId",
                table: "Customers",
                newName: "MembershipTypeFK");

            migrationBuilder.AddColumn<int>(
                name: "GenreFK",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MembershipTypes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Genres",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreFK",
                table: "Movies",
                column: "GenreFK");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MembershipTypeFK",
                table: "Customers",
                column: "MembershipTypeFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MembershipTypes_MembershipTypeFK",
                table: "Customers",
                column: "MembershipTypeFK",
                principalTable: "MembershipTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreFK",
                table: "Movies",
                column: "GenreFK",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Customers_CustomerFK",
                table: "Rentals",
                column: "CustomerFK",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Movies_MovieFK",
                table: "Rentals",
                column: "MovieFK",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MembershipTypes_MembershipTypeFK",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreFK",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Customers_CustomerFK",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Movies_MovieFK",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreFK",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Customers_MembershipTypeFK",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "GenreFK",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "MovieFK",
                table: "Rentals",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "CustomerFK",
                table: "Rentals",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_MovieFK",
                table: "Rentals",
                newName: "IX_Rentals_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_CustomerFK",
                table: "Rentals",
                newName: "IX_Rentals_CustomerId");

            migrationBuilder.RenameColumn(
                name: "MembershipTypeFK",
                table: "Customers",
                newName: "MembershipTypeId");

            migrationBuilder.AddColumn<byte>(
                name: "GenreId",
                table: "Movies",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Id",
                table: "MembershipTypes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<byte>(
                name: "Id",
                table: "Genres",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsSubscribedToNewsletter = table.Column<bool>(type: "INTEGER", nullable: false),
                    MembershipTypeId = table.Column<byte>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GenreId = table.Column<byte>(type: "INTEGER", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    NumberAvailable = table.Column<byte>(type: "INTEGER", nullable: false),
                    NumberInStock = table.Column<byte>(type: "INTEGER", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_GenreId",
                table: "Movie",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genre_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Customer_CustomerId",
                table: "Rentals",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Movie_MovieId",
                table: "Rentals",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
