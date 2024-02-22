using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TasksAPI.Data;
using TasksAPI.Models;
using TasksAPI.Services.Interfaces;
using TasksAPI.ViewModels;

namespace TasksAPI.Controllers
{
    [Route("v1")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        /// <summary>
        /// Busca todos os projetos criados
        /// </summary>
        /// <returns>Todos os projetos criados</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("projeto")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var projetos = await _projetoService.GetAll();
            return Ok(projetos);
        }

        /// <summary>
        /// Busca projeto por Id
        /// </summary>
        /// <returns>Devolve o projeto buscado pelo Id</returns>
        /// <param name="id">Id do projeto</param>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("projeto/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdOrName([FromRoute] int id)
        {
            var projeto = await _projetoService.GetById(id);
            return Ok(projeto);
        }

        /// <summary>
        /// Salva um novo projeto
        /// </summary>      
        /// <remarks>
        /// Exemplo de request(OBS: não é obrigatorio enviar uma Tarefa nesse POST):
        ///
        ///     POST /projeto
        ///     {
        ///           "nomeProjeto": "Projeto Exemplo",
        ///           "usuario": "Usuario Exemplo",
        ///           "tarefas": [
        ///           {
        ///             "nomeTarefa": "Tarefa Exemplo",
        ///             "descricaoTarefa": "Descrição da tarefa exemplo",
        ///             "status": "Pendente",
        ///             "prioridade": "Baixa",
        ///             "projetoId": 1,
        ///             "usuario": "Usuario Exemplo"
        ///           }
        ///           ]
        ///      }        
        /// </remarks>
        /// <param name="viewModel"></param>
        /// <response code="201">Criado</response>
        /// <response code="400">Erro</response>
        [HttpPost]
        [Route("projeto")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ProjetoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try 
            {
                var projeto = new Projeto
                {
                    NomeProjeto = viewModel.NomeProjeto,
                    Usuario = viewModel.Usuario,
                    Tarefas = viewModel.Tarefas
                };

                await _projetoService.Post(projeto);
                return Created("Projeto Criado", viewModel.NomeProjeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        /// <summary>
        /// Atualiza um projeto
        /// </summary>      
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST /projeto
        ///     {
        ///           "nomeProjeto": "Projeto Exemplo",
        ///           "usuario": "Usuario Exemplo"                 
        ///      }        
        /// </remarks>
        /// <param name="viewModel"></param>
        /// <param name="id">Id do projeto</param>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro</response>
        /// <response code="404">Não existe</response>
        [HttpPut]
        [Route("projeto/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] ProjetoPutViewModel viewModel, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();            

            try
            {
                var projeto = await _projetoService.GetById(id);
                if (projeto == null)
                    return NotFound("Projeto não encontrado");

                projeto.NomeProjeto = viewModel.NomeProjeto;
                projeto.Usuario = viewModel.Usuario;
                
                await _projetoService.Put(projeto);
                return Ok(projeto);
            }
            catch (Exception)
            {
                return BadRequest();
            }            
        }

        /// <summary>
        /// Deleta um projeto por Id
        /// </summary>
        /// <returns>Devolve o projeto buscado pelo Id</returns>
        /// <param name="id">Id do projeto</param>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro</response>
        /// <response code="404">Não existe</response>
        [HttpDelete("projeto/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var projeto = await _projetoService.GetById(id);
                if (projeto == null)
                    return NotFound("Projeto não encontrado");

                if(projeto.Tarefas.Any(x => x.Status != Models.Enums.StatusTarefa.Concluida))
                    return BadRequest("Somente é possivel excluir um projeto quando todas tarefas estão concluidas, ou você deve excluir a tarefa que tem status diferente de Comcluido");
            
                await _projetoService.Delete(projeto);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
