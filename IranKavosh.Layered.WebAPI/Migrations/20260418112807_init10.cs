using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "Financial",
                table: "PriceList",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "Financial",
                table: "PriceList",
                newName: "Description");
        }
    }
}
