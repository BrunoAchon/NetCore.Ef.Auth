﻿using WS.Core.Shared.ModelViews.AspNetClient;
using WS.Mananger.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetClientController : ControllerBase
    {
        private readonly IAspNetClientManager _aspNetClientManager;
        private readonly ILogger<AspNetClientController> _logger;

        public AspNetClientController(IAspNetClientManager aspNetClientManager, ILogger<AspNetClientController> logger)
        {
            _aspNetClientManager = aspNetClientManager;
            _logger = logger;
        }

        /// <summary>
        /// Obter lista de todos os clientes
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var clients = await _aspNetClientManager.GetAspNetClientsAsync();
            if (clients.Any())
            {
                return Ok(clients);
            }
            return NotFound();
        }

        /// <summary>
        /// Obter um cliente consultado pelo ID
        /// </summary>
        /// <param name="id" example="1">Id do cliente</param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _aspNetClientManager.GetAspNetClientAsync(id);
            if (client == null) // Não existem Clientes
            {
                return NotFound();
            }
            if (client.ClientId == 0) // o codigo informado é zero
            {
                return NotFound();
            }
            return Ok(client);
        }

        /// <summary>
        /// Inserir um novo cliente
        /// </summary>
        /// <param name="aspNetClientNovo"></param>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(AspNetClientNovo aspNetClientNovo)
        {
            _logger.LogInformation("Parametros: {@aspNetClientNovo}", aspNetClientNovo);

            AspNetClientView aspNetClientInserido;
            using (Operation.Time("Tempo de inclusão do cliente"))
            {
                aspNetClientInserido = await _aspNetClientManager.InsertAspNetClientAsync(aspNetClientNovo);
            }
            return CreatedAtAction(nameof(Get), new { id = aspNetClientInserido.ClientId }, aspNetClientInserido );
        }

        /// <summary>
        ///  Alterar um cliente existente
        /// </summary>
        /// <param name="aspNetClientAlterar"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(AspNetClientAlterar aspNetClientAlterar)
        {
            _logger.LogInformation("Parametros: {@aspNetClientAlterar}", aspNetClientAlterar);

            var aspNetClientAtualizado = await _aspNetClientManager.UpdateAspNetClientAsync(aspNetClientAlterar);
            if (aspNetClientAtualizado == null)
            {
                return NotFound();
            }
            return Ok(aspNetClientAtualizado);

        }

        /// <summary>
        /// Excluir um cliente existente
        /// </summary>
        /// <param name="id" example="1">Id do cliente</param>
        /// <remarks>Ao excluir um cliente o mesmo perderá todos os acessos aos serviços do software,
        /// use esta opção somente se cadastrar algo errado e deseja excluir para fazer novamente</remarks>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Parametros: {@id}", id);

            var aspNetClientExcluido = await _aspNetClientManager.DeleteAspNetClientAsync(id);
            if (aspNetClientExcluido == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
