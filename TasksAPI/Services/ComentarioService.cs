using TasksAPI.Models;
using TasksAPI.Repositories;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Services.Interfaces;

namespace TasksAPI.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        public ComentarioService(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }
        public async Task Post(Comentarios comentarios)
        {          
            await _comentarioRepository.PostAsync(comentarios);
            //var historico = new HistoricoTarefa
            //{
            //    ProjetoId = tarefa.ProjetoId,
            //    TarefaId = comentarios.TarefaId,
            //    Usuario = tarefa.Usuario,
            //    Modificacao = $"Alteração NomeTarefa:{tarefa.NomeTarefa}, DescriçãoTarefa: {tarefa.DescricaoTarefa}, Status: {tarefa.Status}, Usuario: {tarefa.Usuario}"
            //};
        }
    }
}
