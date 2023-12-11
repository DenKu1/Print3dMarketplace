using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.PrintersAPI.Migrations;

    /// <inheritdoc />
    public partial class Add_Printers_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("2a5606d3-d4c2-49e9-9ac9-515a9e405241"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("368f3f6e-a75a-4062-8cdd-3dd58aa54c68"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("6166c1a4-dd1d-4949-b322-3501d7081720"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("83d84d40-e1f7-4934-b378-914e95cc13aa"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("a467d589-f2bd-4641-aa16-18dc6baaed87"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("f7c066e1-e400-494f-8715-5c765ae97d13"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("fbf46e86-ebc2-4cf7-b26a-d7c13e82a926"));

            migrationBuilder.DeleteData(
                table: "TemplatePrinters",
                keyColumn: "Id",
                keyValue: new Guid("7893477c-0a20-418b-9a42-7ec2ada17a72"));

            migrationBuilder.DeleteData(
                table: "TemplatePrinters",
                keyColumn: "Id",
                keyValue: new Guid("b24a4905-d77e-489d-97e6-341ca23fbda3"));

            migrationBuilder.DeleteData(
                table: "TemplatePrinters",
                keyColumn: "Id",
                keyValue: new Guid("c21da304-81fc-4d9f-a8ab-8897fcd8ed6d"));

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplatePrinterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NozzleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintAreaLength = table.Column<double>(type: "float", nullable: false),
                    PrintAreaWidth = table.Column<double>(type: "float", nullable: false),
                    PrintAreaHeight = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Printers_Nozzles_NozzleId",
                        column: x => x.NozzleId,
                        principalTable: "Nozzles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Printers_TemplatePrinters_TemplatePrinterId",
                        column: x => x.TemplatePrinterId,
                        principalTable: "TemplatePrinters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Nozzles",
                columns: new[] { "Id", "Size" },
                values: new object[,]
                {
                    { new Guid("11f2485c-f85b-4d74-af99-c126915282ac"), "0.8mm Nozzle" },
                    { new Guid("1ca6b637-9fd8-445f-b7e2-7df328dbeb28"), "0.6mm Nozzle" },
                    { new Guid("53090a10-2947-4e67-aa0f-d4f2fd0d3815"), "0.4mm Nozzle" },
                    { new Guid("5b70703f-280c-499a-992f-c180aef47fca"), "1.0mm Nozzle" },
                    { new Guid("74dc42de-4b5f-417d-ac2a-e5ba4faf8ad7"), "0.2mm Nozzle" },
                    { new Guid("9f82dc95-221e-4e87-9e5c-0627a68acf8d"), "0.3mm Nozzle" },
                    { new Guid("f1d958c4-9468-4263-a122-c415d6fda1bb"), "0.5mm Nozzle" }
                });

            migrationBuilder.InsertData(
                table: "TemplatePrinters",
                columns: new[] { "Id", "ModelName", "PrintAreaHeight", "PrintAreaLength", "PrintAreaWidth" },
                values: new object[,]
                {
                    { new Guid("01249e15-d84d-406b-b9e0-21fc5e97d412"), "Prusa 360", 200.0, 200.0, 200.0 },
                    { new Guid("067f16d2-e3b3-4d37-8d87-bff0e3e338c9"), "Ender i3", 150.0, 150.0, 150.0 },
                    { new Guid("9c7469f2-420a-4740-ac67-6151d27c6ce1"), "Anet 300", 300.0, 300.0, 300.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Printers_NozzleId",
                table: "Printers",
                column: "NozzleId");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_TemplatePrinterId",
                table: "Printers",
                column: "TemplatePrinterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("11f2485c-f85b-4d74-af99-c126915282ac"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("1ca6b637-9fd8-445f-b7e2-7df328dbeb28"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("53090a10-2947-4e67-aa0f-d4f2fd0d3815"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("5b70703f-280c-499a-992f-c180aef47fca"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("74dc42de-4b5f-417d-ac2a-e5ba4faf8ad7"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("9f82dc95-221e-4e87-9e5c-0627a68acf8d"));

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: new Guid("f1d958c4-9468-4263-a122-c415d6fda1bb"));

            migrationBuilder.DeleteData(
                table: "TemplatePrinters",
                keyColumn: "Id",
                keyValue: new Guid("01249e15-d84d-406b-b9e0-21fc5e97d412"));

            migrationBuilder.DeleteData(
                table: "TemplatePrinters",
                keyColumn: "Id",
                keyValue: new Guid("067f16d2-e3b3-4d37-8d87-bff0e3e338c9"));

            migrationBuilder.DeleteData(
                table: "TemplatePrinters",
                keyColumn: "Id",
                keyValue: new Guid("9c7469f2-420a-4740-ac67-6151d27c6ce1"));

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
    }
