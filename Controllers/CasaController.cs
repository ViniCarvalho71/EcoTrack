using EcoTrack.Entidades;
using EcoTrack.Interfaces;
using EcoTrack.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasaController : ControllerBase
    {
        private readonly IServicoCasa _servico;

        public CasaController(IServicoCasa servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _servico.ObterTodasCasas();

            if (result.Dados != null)
            {
                return Ok(result);
            } else
            {
                return NotFound(result.Mensagem);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _servico.ObterCasaPorId(id);
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
        public async Task<IActionResult> Post([FromBody] Casa casa)
        {
            var result = await _servico.AdicionarCasa(casa);
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
        public async Task<IActionResult> Put([FromBody] Casa casa)
        {
            var result = await _servico.AtualizarCasa(casa);
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
            var result = await _servico.RemoverCasa(id);
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
