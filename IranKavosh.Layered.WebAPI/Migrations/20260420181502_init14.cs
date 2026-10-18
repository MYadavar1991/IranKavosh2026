using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuySellDetail_Product_ProductId",
                schema: "Financial",
                table: "BuySellDetail");

            migrationBuilder.DropColumn(
                name: "BarCode",
                schema: "Financial",
                table: "BuySellDetail");

            migrationBuilder.DropColumn(
                name: "CalculateDateTime",
                schema: "Financial",
                table: "BuySellDetail");

            migrationBuilder.DropColumn(
                name: "ProductCustom",
                schema: "Financial",
                table: "BuySellDetail");

            migrationBuilder.RenameColumn(
                name: "SellPrice",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "PriceSell");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "PriceListId");

            migrationBuilder.RenameColumn(
                name: "Calculated",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "Sell");

            migrationBuilder.RenameColumn(
                name: "BuyPriceIk",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "PriceFinal");

            migrationBuilder.RenameColumn(
                name: "BuyPrice",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "PriceBuy");

            migrationBuilder.RenameIndex(
                name: "IX_BuySellDetail_ProductId",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "IX_BuySellDetail_PriceListId");

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                schema: "Financial",
                table: "Fund",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BuySellDetail_PriceList_PriceListId",
                schema: "Financial",
                table: "BuySellDetail",
                column: "PriceListId",
                principalSchema: "Financial",
                principalTable: "PriceList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuySellDetail_PriceList_PriceListId",
                schema: "Financial",
                table: "BuySellDetail");

            migrationBuilder.DropColumn(
                name: "TableName",
                schema: "Financial",
                table: "Fund");

            migrationBuilder.RenameColumn(
                name: "Sell",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "Calculated");

            migrationBuilder.RenameColumn(
                name: "PriceSell",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "SellPrice");

            migrationBuilder.RenameColumn(
                name: "PriceListId",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "PriceFinal",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "BuyPriceIk");

            migrationBuilder.RenameColumn(
                name: "PriceBuy",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "BuyPrice");

            migrationBuilder.RenameIndex(
                name: "IX_BuySellDetail_PriceListId",
                schema: "Financial",
                table: "BuySellDetail",
                newName: "IX_BuySellDetail_ProductId");

            migrationBuilder.AddColumn<long>(
                name: "BarCode",
                schema: "Financial",
                table: "BuySellDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CalculateDateTime",
                schema: "Financial",
                table: "BuySellDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCustom",
                schema: "Financial",
                table: "BuySellDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BuySellDetail_Product_ProductId",
                schema: "Financial",
                table: "BuySellDetail",
                column: "ProductId",
                principalSchema: "Product",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
