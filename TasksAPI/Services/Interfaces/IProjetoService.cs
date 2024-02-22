using TasksAPI.Models;

namespace TasksAPI.Services.Interfaces
{
    public interface IProjetoService
    {
        Task<List<Projeto>> GetAll();
        Task<Projeto> GetById(int id);
        Task Post(Projeto projeto);
        Task Put(Projeto projeto);
        Task Delete(Projeto projeto);
    }
}
