using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Rodoviaria.Migrations
{
    /// <inheritdoc />
    public partial class NovaColuna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disponivel",
                table: "Cadeira",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "Cadeira");
        }
    }
}
