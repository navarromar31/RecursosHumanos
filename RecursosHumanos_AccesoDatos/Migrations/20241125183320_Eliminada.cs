using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecursosHumanos_AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Eliminada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminada",
                table: "instituciones",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminada",
                table: "instituciones");
        }
    }
}
