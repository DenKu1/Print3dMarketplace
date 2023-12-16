using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintRequestsAPI.Migrations;

    /// <inheritdoc />
    public partial class Add_SubmittedCreators_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("252986fb-f903-4eed-af7f-d9da58c17a26"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("352fccfc-a0f1-49ff-9a72-512d634c1d93"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("54aa8111-d4b4-4fa3-970c-73d26414e2c1"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("70efccfc-ab1f-41f7-bd0e-6ddfab8d4845"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("a5f7bf59-9553-4634-989d-a36701c26c51"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("d53a5231-574f-40d8-b440-bc0297bcb582"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("f4f2e6a7-090b-4668-880b-ceddf8695c60"));

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
                    { new Guid("0775c063-d5e6-4d1c-ae0f-42b41cd1607e"), "Pending" },
                    { new Guid("10b30331-3e10-439d-b7e1-0930cabb8164"), "CreatorSubmission" },
                    { new Guid("204b833f-a545-42b5-8869-510d9d2c2b79"), "UserSubmission" },
                    { new Guid("7b37c506-2712-412d-8db3-92be344cb802"), "Completed" },
                    { new Guid("caa358bc-528d-4726-a604-0069d0bf0b7c"), "Undefined" },
                    { new Guid("d728b2bb-2dc6-45bd-a12b-d75f46a31f6f"), "Canceled" },
                    { new Guid("eb854489-fed9-4184-b1c5-3374281db69c"), "New" }
                });

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

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("0775c063-d5e6-4d1c-ae0f-42b41cd1607e"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("10b30331-3e10-439d-b7e1-0930cabb8164"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("204b833f-a545-42b5-8869-510d9d2c2b79"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("7b37c506-2712-412d-8db3-92be344cb802"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("caa358bc-528d-4726-a604-0069d0bf0b7c"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("d728b2bb-2dc6-45bd-a12b-d75f46a31f6f"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("eb854489-fed9-4184-b1c5-3374281db69c"));

            migrationBuilder.InsertData(
                table: "PrintRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("252986fb-f903-4eed-af7f-d9da58c17a26"), "CreatorSubmission" },
                    { new Guid("352fccfc-a0f1-49ff-9a72-512d634c1d93"), "UserSubmission" },
                    { new Guid("54aa8111-d4b4-4fa3-970c-73d26414e2c1"), "Pending" },
                    { new Guid("70efccfc-ab1f-41f7-bd0e-6ddfab8d4845"), "Completed" },
                    { new Guid("a5f7bf59-9553-4634-989d-a36701c26c51"), "Undefined" },
                    { new Guid("d53a5231-574f-40d8-b440-bc0297bcb582"), "New" },
                    { new Guid("f4f2e6a7-090b-4668-880b-ceddf8695c60"), "Canceled" }
                });
        }
    }
