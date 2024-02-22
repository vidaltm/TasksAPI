using Microsoft.AspNetCore.Server.IIS.Core;
using TasksAPI.Models;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Services.Interfaces;

namespace TasksAPI.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITarefaRepository _tarefaRepository;
        public ProjetoService(IProjetoRepository projetoRepository, IUsuarioRepository usuarioRepository, ITarefaRepository tarefaRepository)
        {
            _projetoRepository = projetoRepository;
            _usuarioRepository = usuarioRepository;
            _tarefaRepository = tarefaRepository;
        }
        public async Task<List<Projeto>> GetAll()
        {
            return await _projetoRepository.GetAllAsync();
        }
        public async Task<Projeto> GetById(int id)
        {
            return await _projetoRepository.GetByIdAsync(id);
        }
        public async Task Post(Projeto projeto)
        {
            var usuarioValido = _usuarioRepository.Get(projeto.Usuario);

            if (usuarioValido)
                await _projetoRepository.PostAsync(projeto);
            else
                throw new Exception("Usuario Invalido");
        }
        public async Task Put(Projeto projeto)
        {
            bool usuarioValido = false;
            if (projeto.Usuario != null)
            {
                usuarioValido = _usuarioRepository.Get(projeto.Usuario);

                if (usuarioValido)
                    await _projetoRepository.PutAsync(projeto);
            }
            else if(projeto.Usuario == null)
            {
                await _projetoRepository.PutAsync(projeto);
            }
            else
                throw new Exception("Usuario Invalido");
        }
        public async Task Delete(Projeto projeto)
        {
            await _projetoRepository.DeleteAsync(projeto);
        }
    }
}
