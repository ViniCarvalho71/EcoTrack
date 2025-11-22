using EcoTrack.Entidades;
using EcoTrack.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AguaController : ControllerBase
    {
        private readonly ServicoAgua _servico;

        public AguaController(ServicoAgua servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _servico.ObterTodasAguas();

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
            var result = await _servico.ObterAguaPorId(id);
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
        public async Task<IActionResult> Post([FromBody] Agua agua)
        {
            var result = await _servico.AdicionarAgua(agua);
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
        public async Task<IActionResult> Put([FromBody] Agua agua)
        {
            var result = await _servico.AtualizarAgua(agua);
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
            var result = await _servico.RemoverAgua(id);
            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Mensagem);
            }
        }
    }
}
