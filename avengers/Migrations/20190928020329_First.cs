using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace avengers.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comics",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Tit_com = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Last_sync = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Creadores",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_com = table.Column<int>(nullable: false),
                    Nom_cre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Rol_cre = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creadores", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comics_Creadores",
                        column: x => x.ID_com,
                        principalTable: "Comics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personajes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_com = table.Column<int>(nullable: false),
                    Nom_per = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personajes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comics_Personajes",
                        column: x => x.ID_com,
                        principalTable: "Comics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creadores_ID_com",
                table: "Creadores",
                column: "ID_com");

            migrationBuilder.CreateIndex(
                name: "IX_Personajes_ID_com",
                table: "Personajes",
                column: "ID_com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Creadores");

            migrationBuilder.DropTable(
                name: "Personajes");

            migrationBuilder.DropTable(
                name: "Comics");
        }
    }
}
