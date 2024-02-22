using TasksAPI.Models;
using TasksAPI.Repositories;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Services.Interfaces;

namespace TasksAPI.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IHistoricoTarefaRepository _historicoTarefaRepository;
        public ComentarioService(IComentarioRepository comentarioRepository, IHistoricoTarefaRepository historicoTarefaRepository)
        {
            _comentarioRepository = comentarioRepository;
            _historicoTarefaRepository = historicoTarefaRepository;
        }
        public async Task Post(Comentarios comentarios)
        {          
            await _comentarioRepository.PostAsync(comentarios);
            var historico = new HistoricoTarefa
            {
                ProjetoId = comentarios.ProjetoId,
                TarefaId = comentarios.TarefaId,
                Usuario = comentarios.Usuario,
                Modificacao = $"Inclusão de novo comentario: {comentarios.Comentario}, pelo Usuario: {comentarios.Usuario}"
            };
            await _historicoTarefaRepository.PostAsync(historico);
        }
    }
}
