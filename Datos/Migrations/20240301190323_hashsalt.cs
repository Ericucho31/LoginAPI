using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class hashsalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordEncriptada",
                table: "Usuario",
                newName: "PasswordSalt");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Usuario",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Usuario",
                newName: "PasswordEncriptada");
        }
    }
}
