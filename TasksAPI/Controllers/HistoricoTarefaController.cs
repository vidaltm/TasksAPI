using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Services;
using TasksAPI.Services.Interfaces;

namespace TasksAPI.Controllers
{
    [Route("v1")]
    [ApiController]
    public class HistoricoTarefaController : ControllerBase
    {
        private readonly IHistoricoTarefaService _historicoService;
        private readonly IUsuarioRepository _usuarioRepository;
        public HistoricoTarefaController(IHistoricoTarefaService historicoService, IUsuarioRepository usuarioRepository)
        {
            _historicoService = historicoService;
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Busca historico de tarefas por usuario
        /// </summary>
        /// <param name="usuarioHistorico">usuario que vai retornar na consulta</param>
        /// <param name="usuario">usuario perfil gerente</param>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("historico/{usuarioHistorico}/{usuario}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHistoricoByUsuario([FromRoute] string usuarioHistorico, string usuario)
        {
            var isGerente = _usuarioRepository.IsGerente(usuario);
            if (!isGerente)
                return BadRequest("Usuario não tem permissões de gerente");

            var historico = _historicoService.GetHistoricoByUsuario(usuarioHistorico)
                .GetAwaiter().GetResult().Where(x => x.DataModificacao >= DateTime.Now.AddDays(-30));
            
            return Ok(historico);
        }
    }
}
