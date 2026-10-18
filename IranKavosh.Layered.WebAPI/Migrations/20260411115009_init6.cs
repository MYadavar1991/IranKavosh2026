using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LogE");

            migrationBuilder.RenameTable(
                name: "ErrorExcep",
                schema: "Log",
                newName: "ErrorExcep",
                newSchema: "LogE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Log");

            migrationBuilder.RenameTable(
                name: "ErrorExcep",
                schema: "LogE",
                newName: "ErrorExcep",
                newSchema: "Log");
        }
    }
}
