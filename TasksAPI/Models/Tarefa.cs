using System.Text.Json.Serialization;
using TasksAPI.Models.Enums;

namespace TasksAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string NomeTarefa { get; set; }
        public string DescricaoTarefa { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Prioridades Prioridade { get; set; }
        public int ProjetoId { get; set; }
        public string Usuario { get; set; }
        public List<Comentarios> Comentario { get; set; }
    }    
}
