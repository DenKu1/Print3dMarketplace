using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintRequestsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFileNamePropertyInPrintRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "PrintRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "PrintRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01e01274-aa17-49c3-83e1-a12f662bd2e0"), "Pending" },
                    { new Guid("16167965-178b-42f4-af8e-67d1db3b7eff"), "New" },
                    { new Guid("38427066-a63d-403b-8447-1d1b34ea8d19"), "Completed" },
                    { new Guid("434a341b-b644-4077-852a-bec760cd5982"), "CreatorSubmission" },
                    { new Guid("48cdc3ef-f47e-4946-9567-e4a1b4e29c8b"), "Canceled" },
                    { new Guid("624ed4a5-7fbc-4765-8eb6-b4d7cf2260a2"), "UserSubmission" },
                    { new Guid("85ca55a5-f3c4-4321-8241-f9a21c1bdd00"), "Undefined" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("01e01274-aa17-49c3-83e1-a12f662bd2e0"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("16167965-178b-42f4-af8e-67d1db3b7eff"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("38427066-a63d-403b-8447-1d1b34ea8d19"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("434a341b-b644-4077-852a-bec760cd5982"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("48cdc3ef-f47e-4946-9567-e4a1b4e29c8b"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("624ed4a5-7fbc-4765-8eb6-b4d7cf2260a2"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("85ca55a5-f3c4-4321-8241-f9a21c1bdd00"));

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "PrintRequests");

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
        }
    }
}
