using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Print3dMarketplace.MaterialsAPI.Migrations;

    /// <inheritdoc />
    public partial class Init_Material_Db_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materials_TemplateMaterials_TemplateMaterialId",
                        column: x => x.TemplateMaterialId,
                        principalTable: "TemplateMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0681bc6c-ca63-4039-a516-36c7e4fa70fb"), "Red" },
                    { new Guid("075c35c2-a807-4d92-bd7d-6e701e4b8b6e"), "Green" },
                    { new Guid("7b185938-1877-4433-b0f4-b8588ee01d56"), "White" },
                    { new Guid("88169965-e520-4157-a3d6-e54dab403aae"), "Blue" }
                });

            migrationBuilder.InsertData(
                table: "TemplateMaterials",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8bbd3668-026e-4f95-8a2b-5939d02dda66"), "PLA" },
                    { new Guid("9c64981b-5b80-4064-a9bb-25debecb38a4"), "PETG" },
                    { new Guid("bffc7b83-c6d2-4490-ab6a-e1fe7d08b230"), "ABS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ColorId",
                table: "Materials",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TemplateMaterialId",
                table: "Materials",
                column: "TemplateMaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "TemplateMaterials");
        }
    }
