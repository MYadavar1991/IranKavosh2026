using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RegisterId",
                schema: "Financial",
                table: "Fund",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Login",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WinUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Login_Personal_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "Base",
                        principalTable: "Personal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fund_RegisterId",
                schema: "Financial",
                table: "Fund",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Login_PersonalId",
                schema: "Base",
                table: "Login",
                column: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fund_Personal_RegisterId",
                schema: "Financial",
                table: "Fund",
                column: "RegisterId",
                principalSchema: "Base",
                principalTable: "Personal",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fund_Personal_RegisterId",
                schema: "Financial",
                table: "Fund");

            migrationBuilder.DropTable(
                name: "Login",
                schema: "Base");

            migrationBuilder.DropIndex(
                name: "IX_Fund_RegisterId",
                schema: "Financial",
                table: "Fund");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                schema: "Financial",
                table: "Fund");
        }
    }
}
