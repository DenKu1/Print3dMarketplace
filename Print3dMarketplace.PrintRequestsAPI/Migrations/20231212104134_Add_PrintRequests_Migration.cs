using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintRequestsAPI.Migrations;

    /// <inheritdoc />
    public partial class Add_PrintRequests_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrintRequestStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintRequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrintRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintRequestStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NozzleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Infill = table.Column<int>(type: "int", nullable: false),
                    PrintAreaLength = table.Column<double>(type: "float", nullable: false),
                    PrintAreaWidth = table.Column<double>(type: "float", nullable: false),
                    PrintAreaHeight = table.Column<double>(type: "float", nullable: false),
                    BorderWidth = table.Column<double>(type: "float", nullable: false),
                    LayerHeight = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseSupports = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintRequests_PrintRequestStatuses_PrintRequestStatusId",
                        column: x => x.PrintRequestStatusId,
                        principalTable: "PrintRequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PrintRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("143e1bd6-b7fd-42dd-8f68-e5c88295af5b"), "Canceled" },
                    { new Guid("3dc0fd22-57d9-4eb5-920c-4fa2ad23c2db"), "Pending" },
                    { new Guid("64437572-7ac6-4b58-8452-646c0f4b26d7"), "Undefined" },
                    { new Guid("81e6e0c5-6aa8-45f1-9c87-cae0da6ebfdc"), "UserSubmission" },
                    { new Guid("a4a15889-5e89-439c-892a-72eede5cd13d"), "New" },
                    { new Guid("e716a373-0797-438d-98e6-e8a6edb39c91"), "Completed" },
                    { new Guid("ed472d4f-0022-450f-a534-9cde34cc1491"), "CreatorSubmission" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrintRequests_PrintRequestStatusId",
                table: "PrintRequests",
                column: "PrintRequestStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrintRequests");

            migrationBuilder.DropTable(
                name: "PrintRequestStatuses");
        }
    }
