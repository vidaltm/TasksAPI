using System.ComponentModel.DataAnnotations;

namespace TasksAPI.ViewModels
{
    public class ComentarioViewModel
    {
        [Required]
        public string Comentario { get; set; }
        [Required]
        public int TarefaId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Usuario { get; set; }
    }
}
