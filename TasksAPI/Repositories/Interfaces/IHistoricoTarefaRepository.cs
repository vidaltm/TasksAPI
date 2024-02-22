using TasksAPI.Models;

namespace TasksAPI.Repositories.Interfaces
{
    public interface IHistoricoTarefaRepository
    {
        Task<List<HistoricoTarefa>> GetHistoricoByUsuarioAsync(string usuario);
        Task PostAsync(HistoricoTarefa historico);
    }
}
