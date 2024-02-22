using TasksAPI.Models;

namespace TasksAPI.Repositories.Interfaces
{
    public interface IComentarioRepository
    {
        Task PostAsync(Comentarios comentarios);
    }
}
