using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Uri");

            migrationBuilder.AddColumn<DateTime>(
                name: "BotLoginDateTime",
                schema: "Base",
                table: "Personal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BotUserId",
                schema: "Base",
                table: "Personal",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "LinkHeader",
                schema: "Uri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkDetail",
                schema: "Uri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkDetail_LinkHeader_LinkHeaderId",
                        column: x => x.LinkHeaderId,
                        principalSchema: "Uri",
                        principalTable: "LinkHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkDetail_LinkHeaderId",
                schema: "Uri",
                table: "LinkDetail",
                column: "LinkHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkDetail",
                schema: "Uri");

            migrationBuilder.DropTable(
                name: "LinkHeader",
                schema: "Uri");

            migrationBuilder.DropColumn(
                name: "BotLoginDateTime",
                schema: "Base",
                table: "Personal");

            migrationBuilder.DropColumn(
                name: "BotUserId",
                schema: "Base",
                table: "Personal");
        }
    }
}
