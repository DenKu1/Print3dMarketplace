using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintersAPI.Migrations;

    /// <inheritdoc />
    public partial class Init_Printer_Db_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nozzles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nozzles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplatePrinters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintAreaLength = table.Column<double>(type: "float", nullable: false),
                    PrintAreaWidth = table.Column<double>(type: "float", nullable: false),
                    PrintAreaHeight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatePrinters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Nozzles",
                columns: new[] { "Id", "Size" },
                values: new object[,]
                {
                    { new Guid("2a5606d3-d4c2-49e9-9ac9-515a9e405241"), "0.6mm Nozzle" },
                    { new Guid("368f3f6e-a75a-4062-8cdd-3dd58aa54c68"), "0.5mm Nozzle" },
                    { new Guid("6166c1a4-dd1d-4949-b322-3501d7081720"), "0.2mm Nozzle" },
                    { new Guid("83d84d40-e1f7-4934-b378-914e95cc13aa"), "0.3mm Nozzle" },
                    { new Guid("a467d589-f2bd-4641-aa16-18dc6baaed87"), "1.0mm Nozzle" },
                    { new Guid("f7c066e1-e400-494f-8715-5c765ae97d13"), "0.4mm Nozzle" },
                    { new Guid("fbf46e86-ebc2-4cf7-b26a-d7c13e82a926"), "0.8mm Nozzle" }
                });

            migrationBuilder.InsertData(
                table: "TemplatePrinters",
                columns: new[] { "Id", "ModelName", "PrintAreaHeight", "PrintAreaLength", "PrintAreaWidth" },
                values: new object[,]
                {
                    { new Guid("7893477c-0a20-418b-9a42-7ec2ada17a72"), "Anet 300", 300.0, 300.0, 300.0 },
                    { new Guid("b24a4905-d77e-489d-97e6-341ca23fbda3"), "Ender i3", 150.0, 150.0, 150.0 },
                    { new Guid("c21da304-81fc-4d9f-a8ab-8897fcd8ed6d"), "Prusa 360", 200.0, 200.0, 200.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nozzles");

            migrationBuilder.DropTable(
                name: "TemplatePrinters");
        }
    }
