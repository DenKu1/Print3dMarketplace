using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintRequestsAPI.Migrations;

    /// <inheritdoc />
    public partial class Add_Initial_migration : Migration
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
                    { new Guid("01be8f83-ad78-47bc-a1e4-e064f4871c1b"), "Completed" },
                    { new Guid("127ab3c8-907d-45f8-af20-181130a06fd5"), "New" },
                    { new Guid("1b4a0daa-3d27-4d7f-aaaa-fb23732c0e80"), "Undefined" },
                    { new Guid("2b618945-d6c2-4519-85af-8e4af9ae41c5"), "CreatorSubmission" },
                    { new Guid("46a9c9c7-26ce-44dc-8746-74eb82a20f2e"), "UserSubmission" },
                    { new Guid("52241bda-b8b4-42ca-bc69-169f3b913359"), "Canceled" },
                    { new Guid("725488bc-a383-412b-a167-740322c931f3"), "Pending" }
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
