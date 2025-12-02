using EcoTrack.Dtos;
using EcoTrack.Entidades;
using EcoTrack.Interfaces;
using EcoTrack.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AguaController : ControllerBase
    {
        private readonly IServicoAgua _servico;

        public AguaController(IServicoAgua servico)
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
        public async Task<IActionResult> Post([FromBody] AguaCreateDto agua)
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
        public async Task<IActionResult> Put([FromBody] AguaAtualizarDto agua)
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

        [HttpPost("CalcGasto/{id}")]
        public async Task<IActionResult> CalcularGasto(int id, [FromBody] double valor)
        {
            var result = await _servico.CalcularGastoAgua(id, valor);
            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Mensagem);
            }
        }
        [HttpPatch("Limite/{id}")]
        public async Task<IActionResult> AtualizarLimite(int id, [FromBody] double NovoLimite)
        {
            var result = await _servico.AtualizarLimiteAgua(id, NovoLimite);
            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Mensagem);
            }
        }



        [HttpPatch("Quantidade/{id}/Zerar")]
        public async Task<IActionResult> ZerarQuantidade(int id)
        {
            var result = await _servico.ZerarQuantidadeAgua(id);
            if (result.Dados != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Mensagem);
            }
        }

        [HttpPatch("Quantidade/{id}")]
        public async Task<IActionResult> AtualizarQuantidade(int id, [FromBody] double novaQuantidade)
        {
            var result = await _servico.AtualizarQuantidadeAgua(id, novaQuantidade);
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
