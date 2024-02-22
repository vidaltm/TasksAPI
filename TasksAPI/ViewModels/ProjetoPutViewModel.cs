using System.ComponentModel.DataAnnotations;

namespace TasksAPI.ViewModels
{
    public class ProjetoPutViewModel
    {
        [Required]
        public string NomeProjeto { get; set; }        
        public string Usuario { get; set; }
    }
}
