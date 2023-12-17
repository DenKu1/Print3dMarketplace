using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintRequestsAPI.Migrations;

    /// <inheritdoc />
    public partial class Initial_Migration : Migration
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
                    CustomerSubmittedCreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LayerHeight = table.Column<double>(type: "float", nullable: false),
                    Infill = table.Column<int>(type: "int", nullable: false),
                    PrintAreaLength = table.Column<double>(type: "float", nullable: false),
                    PrintAreaWidth = table.Column<double>(type: "float", nullable: false),
                    PrintAreaHeight = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseSupports = table.Column<bool>(type: "bit", nullable: true),
                    WallThickness = table.Column<double>(type: "float", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "SubmittedCreators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedCreators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmittedCreators_PrintRequests_PrintRequestId",
                        column: x => x.PrintRequestId,
                        principalTable: "PrintRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PrintRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9e78957b-352c-40f8-bc3a-327312e0f026"), "CreatorSubmission" },
                    { new Guid("a4c32814-cb53-4222-81eb-2da6de21e33a"), "Canceled" },
                    { new Guid("b74f7ee1-2a40-4150-9d5c-a247eb0dc47f"), "New" },
                    { new Guid("edce0867-a9ec-497f-a49a-1151ea0cfda7"), "CustomerSubmission" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrintRequests_PrintRequestStatusId",
                table: "PrintRequests",
                column: "PrintRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedCreators_PrintRequestId",
                table: "SubmittedCreators",
                column: "PrintRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmittedCreators");

            migrationBuilder.DropTable(
                name: "PrintRequests");

            migrationBuilder.DropTable(
                name: "PrintRequestStatuses");
        }
    }
