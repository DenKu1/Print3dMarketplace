using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.AuthAPI.Migrations;

    /// <inheritdoc />
    public partial class Add_CompanyName_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Creators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("366b776d-42ef-4c9a-ae8f-2615d0ebd633"), null, "Customer", "CUSTOMER" },
                    { new Guid("58d41d70-4185-476c-a0e8-0ee11f1e0970"), null, "Creator", "CREATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("366b776d-42ef-4c9a-ae8f-2615d0ebd633"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("58d41d70-4185-476c-a0e8-0ee11f1e0970"));

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Creators");
        }
    }
