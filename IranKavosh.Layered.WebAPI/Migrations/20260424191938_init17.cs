using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Stocks");

            migrationBuilder.CreateTable(
                name: "ShareHolderHeader",
                schema: "Stocks",
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
                    table.PrimaryKey("PK_ShareHolderHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShareHolderDetail",
                schema: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShareHolderHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stocks = table.Column<float>(type: "real", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_ShareHolderDetail_ShareHolderHeader_ShareHolderHeaderId",
                        column: x => x.ShareHolderHeaderId,
                        principalSchema: "Stocks",
                        principalTable: "ShareHolderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShareHolderDetail_PersonalId",
                schema: "Stocks",
                table: "ShareHolderDetail",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareHolderDetail_ShareHolderHeaderId",
                schema: "Stocks",
                table: "ShareHolderDetail",
                column: "ShareHolderHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareHolderDetail",
                schema: "Stocks");

            migrationBuilder.DropTable(
                name: "ShareHolderHeader",
                schema: "Stocks");
        }
    }
}
