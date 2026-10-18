using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IranKavosh.Layered.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.EnsureSchema(
                name: "Movie");

            migrationBuilder.EnsureSchema(
                name: "Base");

            migrationBuilder.CreateTable(
                name: "Genre",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiActor",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiActor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiAge",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiAge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiChapterType",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiChapterType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiCountry",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiCountry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiGroup",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiLocation",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Root = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirShare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiPrice",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiSellType",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiSellType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalGroup",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSaleType",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSaleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShareHolderHeader",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Unit",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultimediaHeader",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<long>(type: "bigint", nullable: false),
                    NamePersian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsWebDl = table.Column<bool>(type: "bit", nullable: false),
                    IsSubtitle = table.Column<bool>(type: "bit", nullable: false),
                    IsAnimated = table.Column<bool>(type: "bit", nullable: false),
                    IsAnime = table.Column<bool>(type: "bit", nullable: false),
                    AgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorsId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MultimediaGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultimediaHeader_MultiAge_AgeId",
                        column: x => x.AgeId,
                        principalSchema: "Movie",
                        principalTable: "MultiAge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultimediaHeader_MultiCountry_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Movie",
                        principalTable: "MultiCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultimediaHeader_MultiGroup_MultimediaGroupId",
                        column: x => x.MultimediaGroupId,
                        principalSchema: "Movie",
                        principalTable: "MultiGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultimediaHeader_MultiLocation_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Movie",
                        principalTable: "MultiLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MultimediaHeader_MultiPrice_PriceId",
                        column: x => x.PriceId,
                        principalSchema: "Movie",
                        principalTable: "MultiPrice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeMelli = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personal_PersonalGroup_PersonalGroupId",
                        column: x => x.PersonalGroupId,
                        principalSchema: "Base",
                        principalTable: "PersonalGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarCode = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    NameApp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamePrint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inventory = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductSaleType_SaleTypeId",
                        column: x => x.SaleTypeId,
                        principalSchema: "Product",
                        principalTable: "ProductSaleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Unit_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Product",
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreList",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MultimediaHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MultiGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenreList_Genre_MultiGenreId",
                        column: x => x.MultiGenreId,
                        principalSchema: "Movie",
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreList_MultimediaHeader_MultimediaHeaderId",
                        column: x => x.MultimediaHeaderId,
                        principalSchema: "Movie",
                        principalTable: "MultimediaHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultimediaHeaderChapter",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MultimediaHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaHeaderChapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultimediaHeaderChapter_MultimediaHeader_MultimediaHeaderId",
                        column: x => x.MultimediaHeaderId,
                        principalSchema: "Movie",
                        principalTable: "MultimediaHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuySellHeader",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuySell = table.Column<bool>(type: "bit", nullable: false),
                    DocNumber = table.Column<long>(type: "bigint", nullable: true),
                    DateCustom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuySellHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuySellHeader_Personal_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "Base",
                        principalTable: "Personal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefinitiveAccount",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocNumber = table.Column<long>(type: "bigint", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCustom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Debtor = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefinitiveAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefinitiveAccount_Personal_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "Base",
                        principalTable: "Personal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareHolderDetail",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShareHolderHeaderId = table.Column<int>(type: "int", nullable: false),
                    ShareHolderHeaderId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_ShareHolderDetail_ShareHolderHeader_ShareHolderHeaderId1",
                        column: x => x.ShareHolderHeaderId1,
                        principalSchema: "Base",
                        principalTable: "ShareHolderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultimediaChapterDetail",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeaderChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    HDTS = table.Column<bool>(type: "bit", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaChapterDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultimediaChapterDetail_MultiChapterType_ChapterTypeId",
                        column: x => x.ChapterTypeId,
                        principalSchema: "Movie",
                        principalTable: "MultiChapterType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MultimediaChapterDetail_MultimediaHeaderChapter_HeaderChapterId",
                        column: x => x.HeaderChapterId,
                        principalSchema: "Movie",
                        principalTable: "MultimediaHeaderChapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellDetail",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuySellHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCustom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<float>(type: "real", nullable: false),
                    Calculated = table.Column<bool>(type: "bit", nullable: false),
                    CalculateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BarCode = table.Column<long>(type: "bigint", nullable: false),
                    SellPrice = table.Column<long>(type: "bigint", nullable: false),
                    BuyPrice = table.Column<long>(type: "bigint", nullable: false),
                    BuyPriceIk = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellDetail_BuySellHeader_BuySellHeaderId",
                        column: x => x.BuySellHeaderId,
                        principalSchema: "Product",
                        principalTable: "BuySellHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiListPersonal",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Clearing = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiListPersonal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiListPersonal_MultiSellType_SellTypeId",
                        column: x => x.SellTypeId,
                        principalSchema: "Movie",
                        principalTable: "MultiSellType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultiListPersonal_MultimediaChapterDetail_ChapterDetailId",
                        column: x => x.ChapterDetailId,
                        principalSchema: "Movie",
                        principalTable: "MultimediaChapterDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultiListPersonal_Personal_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "Base",
                        principalTable: "Personal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuySellHeader_PersonalId",
                schema: "Product",
                table: "BuySellHeader",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_DefinitiveAccount_PersonalId",
                schema: "Account",
                table: "DefinitiveAccount",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreList_MultiGenreId",
                schema: "Movie",
                table: "GenreList",
                column: "MultiGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreList_MultimediaHeaderId",
                schema: "Movie",
                table: "GenreList",
                column: "MultimediaHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiListPersonal_ChapterDetailId",
                schema: "Movie",
                table: "MultiListPersonal",
                column: "ChapterDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiListPersonal_PersonalId",
                schema: "Movie",
                table: "MultiListPersonal",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiListPersonal_SellTypeId",
                schema: "Movie",
                table: "MultiListPersonal",
                column: "SellTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaChapterDetail_ChapterTypeId",
                schema: "Movie",
                table: "MultimediaChapterDetail",
                column: "ChapterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaChapterDetail_HeaderChapterId",
                schema: "Movie",
                table: "MultimediaChapterDetail",
                column: "HeaderChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaHeader_AgeId",
                schema: "Movie",
                table: "MultimediaHeader",
                column: "AgeId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaHeader_CountryId",
                schema: "Movie",
                table: "MultimediaHeader",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaHeader_LocationId",
                schema: "Movie",
                table: "MultimediaHeader",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaHeader_MultimediaGroupId",
                schema: "Movie",
                table: "MultimediaHeader",
                column: "MultimediaGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaHeader_PriceId",
                schema: "Movie",
                table: "MultimediaHeader",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaHeaderChapter_MultimediaHeaderId",
                schema: "Movie",
                table: "MultimediaHeaderChapter",
                column: "MultimediaHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_PersonalGroupId",
                schema: "Base",
                table: "Personal",
                column: "PersonalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SaleTypeId",
                schema: "Product",
                table: "Product",
                column: "SaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitId",
                schema: "Product",
                table: "Product",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SellDetail_BuySellHeaderId",
                schema: "Product",
                table: "SellDetail",
                column: "BuySellHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_SellDetail_ProductId",
                schema: "Product",
                table: "SellDetail",
                column: "ProductId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefinitiveAccount",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "GenreList",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultiActor",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultiListPersonal",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "SellDetail",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ShareHolderDetail",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Genre",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultiSellType",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultimediaChapterDetail",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "BuySellHeader",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ShareHolderHeader",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "MultiChapterType",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultimediaHeaderChapter",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "Personal",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "ProductSaleType",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Unit",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "MultimediaHeader",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "PersonalGroup",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "MultiAge",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultiCountry",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultiGroup",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultiLocation",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MultiPrice",
                schema: "Movie");
        }
    }
}
