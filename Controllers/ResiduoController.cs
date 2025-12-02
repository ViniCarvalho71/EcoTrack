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

        
        [HttpPatch("Limite/{id}")]
        public async Task<IActionResult> AtualizarLimite(int id, [FromBody] double NovoLimite)
        {
            var result = await _servico.AtualizarLimiteResiduo(id, NovoLimite);
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
            var result = await _servico.ZerarQuantidadeResiduo(id);
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
            var result = await _servico.AtualizarQuantidadeResiduo(id, novaQuantidade);
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