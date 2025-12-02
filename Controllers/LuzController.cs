using EcoTrack.Entidades;
using EcoTrack.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class luzController : ControllerBase
{
    public readonly ServicoLuz _servico;
    public luzController(ServicoLuz servico)
    {
        _servico = servico;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _servico.ObterTodasLuz();
        if (result.Dados != null)
        {
            return Ok(result);

        }
        return NotFound(result.Mensagem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _servico.ObterLuzPorId(id);
        if(result.Dados == null)
        {
            return NotFound(result.Mensagem);
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Luz luz)
    {
        var result = await _servico.AdicionarLuz(luz);

        if (result.Dados == null)
        {
            return BadRequest(result.Mensagem);
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Luz luz)
    {
        var result = await _servico.AtualizarLuz(luz);

        if (result.Dados != null) {
            return Ok(result);
        }
        return BadRequest(result.Mensagem);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _servico.DeletarLuz(id);
        if (result.Dados == null) {
            return BadRequest(result.Mensagem);
        }
        return Ok(result);
    }

    [HttpPost("id")]
    public async Task<IActionResult> CalcularGasto(int id, [FromBody] double valor)
    {
        var result = await _servico.CalcularGastoLuz(id, valor);
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
