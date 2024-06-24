using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRadio_Startup.Migrations
{
    /// <inheritdoc />
    public partial class CambiosMusica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MusicaFilename",
                table: "Musicas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusicaFilename",
                table: "Musicas");
        }
    }
}
