using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.Mananger.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetClientController : ControllerBase
    {
        private readonly IAspNetClientMananger _aspNetClientMananger;
        private readonly ILogger<AspNetClientController> _logger;

        public AspNetClientController(IAspNetClientMananger aspNetClientMananger, ILogger<AspNetClientController> logger)
        {
            _aspNetClientMananger = aspNetClientMananger;
            _logger = logger;
        }

        /// <summary>
        /// Obter lista de todos os clientes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var clients = await _aspNetClientMananger.GetAspNetClientsAsync();
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
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _aspNetClientMananger.GetAspNetClientAsync(id);
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
        [HttpPost]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(AspNetClientNovo aspNetClientNovo)
        {
            _logger.LogInformation("Parametros: {@aspNetClientNovo}", aspNetClientNovo);

            AspNetClientView aspNetClientInserido;
            using (Operation.Time("Tempo de inclusão do cliente"))
            {
                aspNetClientInserido = await _aspNetClientMananger.InsertAspNetClient(aspNetClientNovo);
            }
            return CreatedAtAction(nameof(Get), new { id = aspNetClientInserido.ClientId, aspNetClientInserido });
        }

        /// <summary>
        ///  Alterar um cliente existente
        /// </summary>
        /// <param name="aspNetClientAlterar"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(AspNetClientAlterar aspNetClientAlterar)
        {
            _logger.LogInformation("Parametros: {@aspNetClientAlterar}", aspNetClientAlterar);

            var aspNetClientAtualizado = await _aspNetClientMananger.UpdateAspNetClient(aspNetClientAlterar);
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
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AspNetClientView), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Parametros: {@id}", id);

            using (Operation.Time("Tempo de deleção do cliente"))
            {
                await _aspNetClientMananger.DeleteAspNetClient(id);
            }
            return NoContent();
        }
    }
}
