namespace TasksAPI.Models
{
    public class HistoricoTarefa
    {
        public int Id { get; set; }
        public DateTime DataModificacao { get; set; } = DateTime.Now;
        public string Modificacao { get; set; }
        public string Usuario { get; set; }
        public int TarefaId { get; set; }
        public int ProjetoId { get; set; }
    }
}
