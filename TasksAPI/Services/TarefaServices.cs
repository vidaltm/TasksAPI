using TasksAPI.Models;
using TasksAPI.Repositories;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Services.Interfaces;

namespace TasksAPI.Services
{   
    public class TarefaServices : ITarefaServices
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IHistoricoTarefaRepository _historicoTarefaRepository;
        public TarefaServices(ITarefaRepository tarefaRepository, IUsuarioRepository usuarioRepository, IProjetoRepository projetoRepository, IHistoricoTarefaRepository historicoTarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
            _usuarioRepository = usuarioRepository;
            _projetoRepository = projetoRepository;
            _historicoTarefaRepository = historicoTarefaRepository;
        }
        public async Task<List<Tarefa>> GetTarefasByProject(int projectId)
        {
            return await _tarefaRepository.GetTarefasByProjectAsync(projectId);
        }
        public async Task Post(Tarefa tarefa)
        {
            var usuarioValido = _usuarioRepository.Get(tarefa.Usuario);
            var tarefasProjeto = await _projetoRepository.GetByIdAsync(tarefa.ProjetoId);

            if (tarefasProjeto?.Tarefas?.Count() > 20)
                throw new Exception("Projeto pode somenter ter no máximo 20 tarefas");

            if (usuarioValido)
                await _tarefaRepository.PostAsync(tarefa);
            else
                throw new Exception("Usuario Invalido");
        }
        public async Task Put(Tarefa tarefa)
        {
            bool usuarioValido = false;
            if (tarefa.Usuario != null)
            {
                usuarioValido = _usuarioRepository.Get(tarefa.Usuario);

                if (usuarioValido)
                {
                    var historico = new HistoricoTarefa
                    {
                        ProjetoId = tarefa.ProjetoId,
                        TarefaId = tarefa.Id,
                        Usuario = tarefa.Usuario,
                        Modificacao = $"Alteração NomeTarefa:{tarefa.NomeTarefa}, DescriçãoTarefa: {tarefa.DescricaoTarefa}, Status: {tarefa.Status}, Usuario: {tarefa.Usuario}"
                    };
                    await _tarefaRepository.PutAsync(tarefa);
                    await _historicoTarefaRepository.PostAsync(historico);
                }
            }            
            else
                throw new Exception("Usuario Invalido");
        }
        public async Task Delete(Tarefa tarefa)
        {
            await _tarefaRepository.DeleteAsync(tarefa);
        }
    }
}
