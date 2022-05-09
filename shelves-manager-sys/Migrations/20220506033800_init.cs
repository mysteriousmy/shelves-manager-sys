using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace shelves_manager_sys.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    adminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    adminName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    adminPassword = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    adminRole = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    autoLogin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.adminId);
                });

            migrationBuilder.CreateTable(
                name: "cargos",
                columns: table => new
                {
                    cargoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cargoName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargos", x => x.cargoId);
                });

            migrationBuilder.CreateTable(
                name: "shelves",
                columns: table => new
                {
                    shelveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    shelveName = table.Column<string>(type: "text", nullable: false),
                    isOnline = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shelves", x => x.shelveId);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    tgaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    tagCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.tgaId);
                });

            migrationBuilder.CreateTable(
                name: "shelvesCargos",
                columns: table => new
                {
                    recordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    shelveId = table.Column<int>(type: "int", nullable: false),
                    cargoId = table.Column<int>(type: "int", nullable: false),
                    layer = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    isUse = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shelvesCargos", x => x.recordId);
                    table.ForeignKey(
                        name: "FK_shelvesCargos_cargos_cargoId",
                        column: x => x.cargoId,
                        principalTable: "cargos",
                        principalColumn: "cargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shelvesCargos_shelves_shelveId",
                        column: x => x.shelveId,
                        principalTable: "shelves",
                        principalColumn: "shelveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outCargos",
                columns: table => new
                {
                    outCargoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cargoId = table.Column<int>(type: "int", nullable: false),
                    tagId = table.Column<int>(type: "int", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outCargos", x => x.outCargoId);
                    table.ForeignKey(
                        name: "FK_outCargos_cargos_cargoId",
                        column: x => x.cargoId,
                        principalTable: "cargos",
                        principalColumn: "cargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_outCargos_tags_tagId",
                        column: x => x.tagId,
                        principalTable: "tags",
                        principalColumn: "tgaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "putCargos",
                columns: table => new
                {
                    putCargoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cargoId = table.Column<int>(type: "int", nullable: false),
                    tagId = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_putCargos", x => x.putCargoId);
                    table.ForeignKey(
                        name: "FK_putCargos_cargos_cargoId",
                        column: x => x.cargoId,
                        principalTable: "cargos",
                        principalColumn: "cargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_putCargos_tags_tagId",
                        column: x => x.tagId,
                        principalTable: "tags",
                        principalColumn: "tgaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_outCargos_cargoId",
                table: "outCargos",
                column: "cargoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_outCargos_tagId",
                table: "outCargos",
                column: "tagId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_putCargos_cargoId",
                table: "putCargos",
                column: "cargoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_putCargos_tagId",
                table: "putCargos",
                column: "tagId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shelvesCargos_cargoId",
                table: "shelvesCargos",
                column: "cargoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shelvesCargos_shelveId",
                table: "shelvesCargos",
                column: "shelveId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "outCargos");

            migrationBuilder.DropTable(
                name: "putCargos");

            migrationBuilder.DropTable(
                name: "shelvesCargos");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "cargos");

            migrationBuilder.DropTable(
                name: "shelves");
        }
    }
}
