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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_com = table.Column<int>(nullable: false),
                    ID_per = table.Column<int>(nullable: false),
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
                    ID_per = table.Column<int>(nullable: false),
                    Rol_cre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Nom_cre = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creadores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Personajes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_com = table.Column<int>(nullable: false),
                    ID_per = table.Column<int>(nullable: false),
                    Nom_per = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personajes", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comics");

            migrationBuilder.DropTable(
                name: "Creadores");

            migrationBuilder.DropTable(
                name: "Personajes");
        }
    }
}
