using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews.AspNetModule;
using WS.Mananger.Interfaces.Managers;

namespace WS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetModuleController : ControllerBase
    {
        private readonly IAspNetModuleManager _aspNetModuleMananger;
        private readonly ILogger<AspNetModuleController> _logger;

        public AspNetModuleController(IAspNetModuleManager aspNetModuleMananger, ILogger<AspNetModuleController> logger)
        {
            _aspNetModuleMananger = aspNetModuleMananger;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todos os Modulos cadastrados na base
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(AspNetModuleView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            //return Ok(await _aspNetModuleMananger.GetAspNetModulesAsync());
            var modules = await _aspNetModuleMananger.GetAspNetModulesAsync();
            if (modules.Any())
            {
                return Ok(modules);
            }
            return NotFound();
        }

        /// <summary>
        /// Retorna um Modulo consultado pelo ID
        /// </summary>
        /// <param name="id" example="1">Id do Modulo</param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AspNetModuleView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            //return Ok(await _aspNetModuleMananger.GetAspNetModuleAsync(id));
            var module = await _aspNetModuleMananger.GetAspNetModuleAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            if (module.ModuleId == 0)
            {
                return NotFound();
            }
            return Ok(module);
        }

        /// <summary>
        /// Inserir um novo Modulo
        /// </summary>
        /// <param name="aspNetModuleNovo"></param>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(AspNetModuleView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(AspNetModuleNovo aspNetModuleNovo)
        {
            _logger.LogInformation("Parametros: {@aspNetModuleNovo}", aspNetModuleNovo);

            AspNetModuleView aspNetModuleInserido;
            using (Operation.Time("Tempo de inclusão do Modulo"))
            {
                aspNetModuleInserido = await _aspNetModuleMananger.InsertAspNetModuleAsync(aspNetModuleNovo);
            }
            return CreatedAtAction(nameof(Get), new { id = aspNetModuleInserido.ModuleId }, aspNetModuleInserido );
        }

        /// <summary>
        ///  Alterar um Modulo existente
        /// </summary>
        /// <param name="aspNetModuleAlterar"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(AspNetModuleView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(AspNetModuleAlterar aspNetModuleAlterar)
        {
            _logger.LogInformation("Parametros: {@aspNetModuleAlterar}", aspNetModuleAlterar);

            AspNetModuleView aspNetModuleAtualizado;
            aspNetModuleAtualizado = await _aspNetModuleMananger.UpdateAspNetModuleAsync(aspNetModuleAlterar);
            if (aspNetModuleAtualizado == null)
            {
                return NotFound();
            }
            return Ok(aspNetModuleAtualizado);
        }

        /// <summary>
        /// Excluir um Modulee existente
        /// </summary>
        /// <param name="id" example="1">Id do Modulee</param>
        /// <remarks>Ao excluir um Modulo o mesmo será replicado aos serviços do software,
        /// use esta opção somente se cadastrar algo errado e deseja excluir para fazer novamente</remarks>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Parametros: {@id}", id);
            var aspNetModuleExcluido = await _aspNetModuleMananger.DeleteAspNetModuleAsync(id);
            if (aspNetModuleExcluido == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
