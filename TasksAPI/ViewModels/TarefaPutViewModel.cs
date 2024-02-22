using System.ComponentModel.DataAnnotations;
using TasksAPI.Models.Enums;

namespace TasksAPI.ViewModels
{
    public class TarefaPutViewModel
    {        
        public string? NomeTarefa { get; set; }       
        public string? DescricaoTarefa { get; set; }       
        [Required]
        public int ProjetoId { get; set; }        
        public string? Usuario { get; set; }
        public StatusTarefa? Status { get; set; }
    }
}
