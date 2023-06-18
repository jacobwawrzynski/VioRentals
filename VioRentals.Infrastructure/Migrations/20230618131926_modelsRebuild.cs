using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VioRentals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modelsRebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForeName",
                table: "Customers",
                newName: "Forename");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Forename",
                table: "Customers",
                newName: "ForeName");
        }
    }
}
