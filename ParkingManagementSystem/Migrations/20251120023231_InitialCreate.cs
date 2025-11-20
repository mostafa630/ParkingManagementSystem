using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ParkingManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Long = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdditionalHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tariffs_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    To = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsExtend = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TariffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Id", "Lat", "Long", "Name" },
                values: new object[,]
                {
                    { new Guid("4f0c3b03-2b0b-4a13-b4f2-3aa5e66d0c42"), "51.5074 N", "0.1278 W", "Site B" },
                    { new Guid("5f0c3b03-3b0b-4a13-b4f2-3aa5e66d0c42"), "55.5074 N", "65.1278 W", "Site C" },
                    { new Guid("7c8d24cf-0f3c-4d3f-8014-0b44b0cbbc43"), "40.7128 N", "74.0060 W", "Site A" }
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "Id", "AdditionalHour", "FirstHour", "SiteId" },
                values: new object[,]
                {
                    { new Guid("4f0c3b03-9b0b-4a13-b4f2-3aa5e66d0c42"), 3.00m, 5.00m, new Guid("7c8d24cf-0f3c-4d3f-8014-0b44b0cbbc43") },
                    { new Guid("8f0c3b03-9b0b-4a13-b4f2-3aa5e66d0c42"), 4.00m, 6.00m, new Guid("4f0c3b03-2b0b-4a13-b4f2-3aa5e66d0c42") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_SiteId",
                table: "Tariffs",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SiteId",
                table: "Tickets",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TariffId",
                table: "Tickets",
                column: "TariffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
