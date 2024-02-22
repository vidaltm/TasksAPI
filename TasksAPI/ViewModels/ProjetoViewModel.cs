using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using TasksAPI.Models;

namespace TasksAPI.ViewModels
{
    public class ProjetoViewModel
    {
        [Required]
        public string NomeProjeto { get; set; }
        [Required]
        public string Usuario { get; set; }
        public List<Tarefa>? Tarefas { get; set; }
    }
}
