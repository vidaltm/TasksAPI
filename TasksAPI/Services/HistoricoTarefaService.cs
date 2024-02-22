using TasksAPI.Models;
using TasksAPI.Repositories;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Services.Interfaces;

namespace TasksAPI.Services
{
    public class HistoricoTarefaService : IHistoricoTarefaService
    {
        private readonly IHistoricoTarefaRepository _historicoRepository;

        public HistoricoTarefaService(IHistoricoTarefaRepository historicoRepository)
        {
            _historicoRepository = historicoRepository;
        }
        public async Task<List<HistoricoTarefa>> GetHistoricoByUsuario(string usuario)
        {
            return await _historicoRepository.GetHistoricoByUsuarioAsync(usuario);
        }
    }
}
