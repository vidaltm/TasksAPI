using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksAPI.Migrations
{
    public partial class InclusaoTarefaProjetoHistorico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "HistoricoTarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TarefaId",
                table: "HistoricoTarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "HistoricoTarefas");

            migrationBuilder.DropColumn(
                name: "TarefaId",
                table: "HistoricoTarefas");
        }
    }
}
