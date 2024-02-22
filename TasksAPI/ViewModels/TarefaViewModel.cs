using System.ComponentModel.DataAnnotations;
using TasksAPI.Models.Enums;

namespace TasksAPI.ViewModels
{
    public class TarefaViewModel
    {
        [Required]
        public string NomeTarefa { get; set; }
        [Required]
        public string DescricaoTarefa { get; set; }
        [Required]
        public Prioridades Prioridade { get; set; }
        [Required]
        public int ProjetoId { get; set; }
        [Required]
        public string Usuario { get; set; }
    }
}
