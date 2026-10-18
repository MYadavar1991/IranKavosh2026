using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Code",
                schema: "Financial",
                table: "BuySellDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Financial",
                table: "BuySellDetail");
        }
    }
}
