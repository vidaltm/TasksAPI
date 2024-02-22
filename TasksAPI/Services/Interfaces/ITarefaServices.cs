using TasksAPI.Models;

namespace TasksAPI.Services.Interfaces
{
    public interface ITarefaServices
    {
        Task<List<Tarefa>> GetTarefasByProject(int projectId);
        Task Post(Tarefa tarefa);
        Task Put(Tarefa tarefa);
        Task Delete(Tarefa tarefa);
    }
}
