using TasksAPI.Models;

namespace TasksAPI.Repositories.Interfaces
{
    public interface ITarefaRepository
    {
        Task<List<Tarefa>> GetTarefasByProjectAsync(int projectId);
        Task PostAsync(Tarefa tarefa);
        Task PutAsync(Tarefa tarefa);
        Task DeleteAsync(Tarefa tarefa);
    }
}
