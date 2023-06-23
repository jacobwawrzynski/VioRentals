using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VioRentals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class onionRefactorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MembershipTypes_MembershipTypeFK",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MembershipTypes");

            migrationBuilder.RenameColumn(
                name: "Returned",
                table: "Rentals",
                newName: "IsReturned");

            migrationBuilder.RenameColumn(
                name: "MembershipTypeFK",
                table: "Customers",
                newName: "MembershipDetailsFK");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_MembershipTypeFK",
                table: "Customers",
                newName: "IX_Customers_MembershipDetailsFK");

            migrationBuilder.AlterColumn<byte>(
                name: "NumberInStock",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenreFK",
                table: "Movies",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MembershipType",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MembershipTypes_MembershipDetailsFK",
                table: "Customers",
                column: "MembershipDetailsFK",
                principalTable: "MembershipTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MembershipTypes_MembershipDetailsFK",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MembershipType",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "IsReturned",
                table: "Rentals",
                newName: "Returned");

            migrationBuilder.RenameColumn(
                name: "MembershipDetailsFK",
                table: "Customers",
                newName: "MembershipTypeFK");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_MembershipDetailsFK",
                table: "Customers",
                newName: "IX_Customers_MembershipTypeFK");

            migrationBuilder.AlterColumn<byte>(
                name: "NumberInStock",
                table: "Movies",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "GenreFK",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MembershipTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MembershipTypes_MembershipTypeFK",
                table: "Customers",
                column: "MembershipTypeFK",
                principalTable: "MembershipTypes",
                principalColumn: "Id");
        }
    }
}
