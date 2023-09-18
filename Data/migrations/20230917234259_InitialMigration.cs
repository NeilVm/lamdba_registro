using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace registro.Data.migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "registro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    prinom = table.Column<string>(type: "longtext", nullable: false),
                    segnom = table.Column<string>(type: "longtext", nullable: false),
                    apepater = table.Column<string>(type: "longtext", nullable: false),
                    apemater = table.Column<string>(type: "longtext", nullable: false),
                    correo = table.Column<string>(type: "longtext", nullable: false),
                    contrasena = table.Column<string>(type: "longtext", nullable: false),
                    tipodoc = table.Column<string>(type: "longtext", nullable: false),
                    documento = table.Column<string>(type: "longtext", nullable: false),
                    fecnac = table.Column<string>(type: "longtext", nullable: false),
                    sexo = table.Column<string>(type: "longtext", nullable: false),
                    peso = table.Column<string>(type: "longtext", nullable: false),
                    alt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registro", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registro");
        }
    }
}
