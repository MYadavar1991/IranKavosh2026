using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BFactorHeader",
                schema: "Bot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BFactorHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BFactorHeader_Personal_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "Base",
                        principalTable: "Personal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BFactorDetail",
                schema: "Bot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<float>(type: "real", nullable: false),
                    PriceSell = table.Column<long>(type: "bigint", nullable: false),
                    PriceUnit = table.Column<long>(type: "bigint", nullable: false),
                    FactorHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BFactorDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BFactorDetail_BFactorHeader_FactorHeaderId",
                        column: x => x.FactorHeaderId,
                        principalSchema: "Bot",
                        principalTable: "BFactorHeader",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BFactorDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BFactorDetail_FactorHeaderId",
                schema: "Bot",
                table: "BFactorDetail",
                column: "FactorHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_BFactorDetail_ProductId",
                schema: "Bot",
                table: "BFactorDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BFactorHeader_BuyerId",
                schema: "Bot",
                table: "BFactorHeader",
                column: "BuyerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BFactorDetail",
                schema: "Bot");

            migrationBuilder.DropTable(
                name: "BFactorHeader",
                schema: "Bot");
        }
    }
}
