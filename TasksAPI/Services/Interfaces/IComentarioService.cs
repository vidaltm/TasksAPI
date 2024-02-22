using TasksAPI.Models;

namespace TasksAPI.Services.Interfaces
{
    public interface IComentarioService
    {
        Task Post(Comentarios comentarios);
    }
}
