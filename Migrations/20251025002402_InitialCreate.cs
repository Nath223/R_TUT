using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R_TUT.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Vacío: no crear nada porque la BD ya existe
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Vacío: no eliminar nada
        }
    }
}
