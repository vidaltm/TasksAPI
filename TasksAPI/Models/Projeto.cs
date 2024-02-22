using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        public string NomeProjeto { get; set; }
        public string Usuario { get; set; }
        public List<Tarefa>? Tarefas { get; set; } = new();
    }
}
