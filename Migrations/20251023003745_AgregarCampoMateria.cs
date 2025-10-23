using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R_TUT.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoMateria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Materia",
                table: "DOCUMENTOS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Materia",
                table: "DOCUMENTOS");
        }
    }
}
