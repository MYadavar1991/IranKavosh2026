using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareHolderDetail",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "ShareHolderHeader",
                schema: "Base");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShareHolderHeader",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareHolderHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShareHolderDetail",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShareHolderHeaderId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ShareHolderHeaderId = table.Column<int>(type: "int", nullable: false),
                    Stocks = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareHolderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareHolderDetail_Personal_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "Base",
                        principalTable: "Personal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareHolderDetail_ShareHolderHeader_ShareHolderHeaderId1",
                        column: x => x.ShareHolderHeaderId1,
                        principalSchema: "Base",
                        principalTable: "ShareHolderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShareHolderDetail_PersonalId",
                schema: "Base",
                table: "ShareHolderDetail",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareHolderDetail_ShareHolderHeaderId1",
                schema: "Base",
                table: "ShareHolderDetail",
                column: "ShareHolderHeaderId1");
        }
    }
}
