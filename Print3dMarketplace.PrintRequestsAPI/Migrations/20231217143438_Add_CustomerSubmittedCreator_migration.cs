using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintRequestsAPI.Migrations;

    /// <inheritdoc />
    public partial class Add_CustomerSubmittedCreator_migration : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerSubmittedCreatorId",
                table: "PrintRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "PrintRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00338f01-69dc-487f-ae62-0665df103b07"), "UserSubmission" },
                    { new Guid("0b0a26bf-a0c9-4549-8e64-026176cc7dcf"), "New" },
                    { new Guid("83a02861-cdb9-4448-9d81-6737003c2051"), "Pending" },
                    { new Guid("86d7a658-7585-4531-9ad8-9f26cf173540"), "Completed" },
                    { new Guid("9896da6e-6748-440c-a8e3-a2acb8c69876"), "Undefined" },
                    { new Guid("9f74103b-0a68-45d6-a4e0-a8c6d0872a6c"), "CreatorSubmission" },
                    { new Guid("ab44cb20-bd9f-4186-a694-4550998ae583"), "Canceled" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("00338f01-69dc-487f-ae62-0665df103b07"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("0b0a26bf-a0c9-4549-8e64-026176cc7dcf"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("83a02861-cdb9-4448-9d81-6737003c2051"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("86d7a658-7585-4531-9ad8-9f26cf173540"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("9896da6e-6748-440c-a8e3-a2acb8c69876"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("9f74103b-0a68-45d6-a4e0-a8c6d0872a6c"));

            migrationBuilder.DeleteData(
                table: "PrintRequestStatuses",
                keyColumn: "Id",
                keyValue: new Guid("ab44cb20-bd9f-4186-a694-4550998ae583"));

            migrationBuilder.DropColumn(
                name: "CustomerSubmittedCreatorId",
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
