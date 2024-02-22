using TasksAPI.Models;

namespace TasksAPI.Services.Interfaces
{
    public interface IHistoricoTarefaService
    {
        Task<List<HistoricoTarefa>> GetHistoricoByUsuario(string usuario);
    }
}
