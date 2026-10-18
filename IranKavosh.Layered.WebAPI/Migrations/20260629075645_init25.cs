using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "IMDB",
                schema: "Movie",
                table: "MultimediaHeader",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                schema: "Movie",
                table: "Genre",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMDB",
                schema: "Movie",
                table: "MultimediaHeader");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                schema: "Movie",
                table: "Genre");
        }
    }
}
