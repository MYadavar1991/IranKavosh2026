using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BFactorDetail_BFactorHeader_FactorHeaderId",
                schema: "Bot",
                table: "BFactorDetail");

            migrationBuilder.AlterColumn<Guid>(
                name: "FactorHeaderId",
                schema: "Bot",
                table: "BFactorDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BFactorDetail_BFactorHeader_FactorHeaderId",
                schema: "Bot",
                table: "BFactorDetail",
                column: "FactorHeaderId",
                principalSchema: "Bot",
                principalTable: "BFactorHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BFactorDetail_BFactorHeader_FactorHeaderId",
                schema: "Bot",
                table: "BFactorDetail");

            migrationBuilder.AlterColumn<Guid>(
                name: "FactorHeaderId",
                schema: "Bot",
                table: "BFactorDetail",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_BFactorDetail_BFactorHeader_FactorHeaderId",
                schema: "Bot",
                table: "BFactorDetail",
                column: "FactorHeaderId",
                principalSchema: "Bot",
                principalTable: "BFactorHeader",
                principalColumn: "Id");
        }
    }
}
