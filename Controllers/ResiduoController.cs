using EcoTrack.Entidades;
using EcoTrack.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResiduoController : Controller
    {
        public readonly ServicoResiduo _servico;

        public ResiduoController(ServicoResiduo servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _servico.ObterTodosResiduos();

            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Mensagem);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _servico.ObterResiduoPorId(id);

            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Mensagem);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Residuo residuo)
        {
            var result = await _servico.AdicionarResiduo(residuo);

            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Mensagem);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Residuo residuo)
        {
            var result = await _servico.AtualizarResiduo(residuo);

            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Mensagem);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _servico.DeletarResiduo(id);

            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Mensagem);
            }
        }
    }
}