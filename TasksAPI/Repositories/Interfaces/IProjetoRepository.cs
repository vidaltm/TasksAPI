using TasksAPI.Models;

namespace TasksAPI.Repositories.Interfaces
{
    public interface IProjetoRepository
    {
        Task<List<Projeto>> GetAllAsync();
        Task<Projeto> GetByIdAsync(int id);
        Task PostAsync(Projeto projeto);
        Task PutAsync(Projeto projeto);
        Task DeleteAsync(Projeto projeto);
    }
}
