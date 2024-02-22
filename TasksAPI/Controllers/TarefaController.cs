using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TasksAPI.Models;
using TasksAPI.Services.Interfaces;
using TasksAPI.ViewModels;

namespace TasksAPI.Controllers
{
    [Route("v1")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaServices _tarefaServices;
        private readonly IProjetoService _projetoService;
        private readonly IComentarioService _comentarioService;
        public TarefaController(ITarefaServices tarefaServices, IProjetoService projetoService, IComentarioService comentarioService)
        {
            _tarefaServices = tarefaServices;
            _projetoService = projetoService;
            _comentarioService = comentarioService;
        }

        /// <summary>
        /// Busca todas as tarefas de um projeto
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        /// <returns>Todos as tarefas criadas para um projeto</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("tarefa/{projectId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTarefasByProject([FromRoute] int projectId)
        {
            var projetos = await _tarefaServices.GetTarefasByProject(projectId);
            return Ok(projetos);
        }

        /// <summary>
        /// Salva uma nova tarefa
        /// </summary>      
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST /tarefa
        ///     {
        ///           "nomeTarefa": "Tarefa Exemplo",
        ///           "descricaoTarefa": "Descrição Exemplo",
        ///           "prioridade": "Baixa",
        ///           "projetoId": 0,
        ///           "usuario": "Usuario Exemplo"
        ///     }        
        /// </remarks>
        /// <param name="viewModel"></param>
        /// <response code="201">Criado</response>
        /// <response code="400">Erro</response>
        [HttpPost]
        [Route("tarefa")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] TarefaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var tarefa = new Tarefa
                {
                    NomeTarefa = viewModel.NomeTarefa,
                    DescricaoTarefa = viewModel.DescricaoTarefa,
                    Prioridade = viewModel.Prioridade,
                    ProjetoId = viewModel.ProjetoId,
                    Usuario = viewModel.Usuario
                };

                await _tarefaServices.Post(tarefa);
                return Created("Tarefa Criada", viewModel.NomeTarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma tarefa de um projeto
        /// </summary>      
        /// <remarks>
        /// Exemplo de request(O unico campo obrigatório é o ProjetoId):
        ///
        ///     POST /tarefa
        ///     {
        ///           "nomeProjeto": "Projeto Exemplo",
        ///           "usuario": "Usuario Exemplo",
        ///           "projetoId": 1,
        ///           "usuario": "Usuario exemplo",
        ///           "status": "Pendente"
        ///     }        
        /// </remarks>
        /// <param name="viewModel"></param>
        /// <param name="id">Id da tarefa</param>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro</response>
        /// <response code="404">Não existe</response>
        [HttpPut]
        [Route("tarefa/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] TarefaPutViewModel viewModel, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var projeto = await _projetoService.GetById(viewModel.ProjetoId);
                if (projeto == null)
                    return NotFound("Projeto não encontrado");

                var tarefaProjeto = projeto.Tarefas.Where(x => x.Id == id).FirstOrDefault();
                if (tarefaProjeto == null)
                    return NotFound("Tarefa não encontrada");

                tarefaProjeto.NomeTarefa = viewModel.NomeTarefa ?? tarefaProjeto.NomeTarefa;
                tarefaProjeto.DescricaoTarefa = viewModel.DescricaoTarefa ?? tarefaProjeto.DescricaoTarefa;
                tarefaProjeto.Usuario = viewModel.Usuario ?? tarefaProjeto.Usuario;
                tarefaProjeto.Status = viewModel.Status ?? tarefaProjeto.Status;
                
                await _tarefaServices.Put(tarefaProjeto);
                return Ok(projeto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deleta uma tarefa de um projeto
        /// </summary>
        /// <param name="projectId">Id do projeto</param>
        /// <param name="id">Id da tarefa</param>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro</response>
        /// <response code="404">Não existe</response>
        [HttpDelete("tarefa/{projectId}/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int projectId, int id)
        { 
            try
            {
                var projeto = await _projetoService.GetById(projectId);
                if (projeto == null)
                    return NotFound("Projeto não encontrado");

                var tarefaProjeto = projeto.Tarefas.Where(x => x.Id == id).FirstOrDefault();
                if (tarefaProjeto == null)
                    return NotFound("Tarefa não encontrada");

                await _tarefaServices.Delete(tarefaProjeto);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Salva um novo comentario na tarefa
        /// </summary>      
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST /tarefa/comentario
        ///     {
        ///           "comentario": "Comentario Exemplo",
        ///           "tarefaId": 1,                   
        ///           "projetoId": 1,
        ///     }        
        /// </remarks>
        /// <param name="viewModel"></param>
        /// <response code="201">Criado</response>
        /// <response code="400">Erro</response>
        [HttpPost]
        [Route("tarefa/comentario")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostComentario([FromBody] ComentarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var projeto = await _projetoService.GetById(viewModel.ProjectId);
                if (projeto == null)
                    return NotFound("Projeto não encontrado");

                var tarefaProjeto = projeto.Tarefas.Where(x => x.Id == viewModel.TarefaId).FirstOrDefault();
                if (tarefaProjeto == null)
                    return NotFound("Tarefa não encontrada");

                var comentario = new Comentarios
                {
                    Comentario = viewModel.Comentario,
                    TarefaId = viewModel.TarefaId,
                    ProjetoId = viewModel.ProjectId,
                    Usuario = viewModel.Usuario
                };

                await _comentarioService.Post(comentario);
                return Created("Comentario adicionado", viewModel.Comentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
