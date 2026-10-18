using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fund_FundType_FundTypeId",
                schema: "Financial",
                table: "Fund");

            migrationBuilder.DropIndex(
                name: "IX_Fund_FundTypeId",
                schema: "Financial",
                table: "Fund");

            migrationBuilder.DropColumn(
                name: "FundTypeId",
                schema: "Financial",
                table: "Fund");

            migrationBuilder.AddColumn<Guid>(
                name: "FundTypeId",
                schema: "Product",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_FundTypeId",
                schema: "Product",
                table: "Product",
                column: "FundTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_FundType_FundTypeId",
                schema: "Product",
                table: "Product",
                column: "FundTypeId",
                principalSchema: "Financial",
                principalTable: "FundType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_FundType_FundTypeId",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_FundTypeId",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FundTypeId",
                schema: "Product",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "FundTypeId",
                schema: "Financial",
                table: "Fund",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fund_FundTypeId",
                schema: "Financial",
                table: "Fund",
                column: "FundTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fund_FundType_FundTypeId",
                schema: "Financial",
                table: "Fund",
                column: "FundTypeId",
                principalSchema: "Financial",
                principalTable: "FundType",
                principalColumn: "Id");
        }
    }
}
