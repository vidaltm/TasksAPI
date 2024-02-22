using TasksAPI.Models;

namespace TasksAPI.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        bool Get(string userName);
        bool IsGerente(string userName);
    }
}
