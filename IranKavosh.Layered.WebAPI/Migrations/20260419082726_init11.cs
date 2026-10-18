using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_SalesService_SaleTypeId",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropTable(
                name: "SalesService",
                schema: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_SaleTypeId",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SaleTypeId",
                schema: "Product",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SaleTypeId",
                schema: "Product",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SalesService",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FundTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesService_FundType_FundTypeId",
                        column: x => x.FundTypeId,
                        principalSchema: "Financial",
                        principalTable: "FundType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_SaleTypeId",
                schema: "Product",
                table: "Product",
                column: "SaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesService_FundTypeId",
                schema: "Product",
                table: "SalesService",
                column: "FundTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SalesService_SaleTypeId",
                schema: "Product",
                table: "Product",
                column: "SaleTypeId",
                principalSchema: "Product",
                principalTable: "SalesService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
