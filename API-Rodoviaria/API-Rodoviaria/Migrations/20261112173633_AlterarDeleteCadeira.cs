using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Rodoviaria.Migrations
{
    /// <inheritdoc />
    public partial class AlterarDeleteCadeira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cadeira_Onibus_FkOnibus",
                table: "Cadeira");

            migrationBuilder.AddForeignKey(
                name: "FK_Cadeira_Onibus_FkOnibus",
                table: "Cadeira",
                column: "FkOnibus",
                principalTable: "Onibus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cadeira_Onibus_FkOnibus",
                table: "Cadeira");

            migrationBuilder.AddForeignKey(
                name: "FK_Cadeira_Onibus_FkOnibus",
                table: "Cadeira",
                column: "FkOnibus",
                principalTable: "Onibus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
