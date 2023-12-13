using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintRequestsAPI.Migrations;

    /// <inheritdoc />
    public partial class Update_PrintRequest_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("143e1bd6-b7fd-42dd-8f68-e5c88295af5b"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("3dc0fd22-57d9-4eb5-920c-4fa2ad23c2db"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("64437572-7ac6-4b58-8452-646c0f4b26d7"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("81e6e0c5-6aa8-45f1-9c87-cae0da6ebfdc"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("a4a15889-5e89-439c-892a-72eede5cd13d"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("e716a373-0797-438d-98e6-e8a6edb39c91"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("ed472d4f-0022-450f-a534-9cde34cc1491"));

            migrationBuilder.DropColumn(
                name: "BorderWidth",
                table: "PrintRequests");

            migrationBuilder.DropColumn(
                name: "NozzleId",
                table: "PrintRequests");

            migrationBuilder.AlterColumn<bool>(
                name: "UseSupports",
                table: "PrintRequests",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "PrintRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<double>(
                name: "WallThickness",
                table: "PrintRequests",
                type: "float",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "WallThickness",
                table: "PrintRequests");

            migrationBuilder.AlterColumn<bool>(
                name: "UseSupports",
                table: "PrintRequests",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "PrintRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BorderWidth",
                table: "PrintRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "NozzleId",
                table: "PrintRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }
    }
