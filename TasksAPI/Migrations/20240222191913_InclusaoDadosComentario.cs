using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksAPI.Migrations
{
    public partial class InclusaoDadosComentario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "Comentarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Comentarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Comentarios");
        }
    }
}
