using EcoTrack.Entidades;
using EcoTrack.Interfaces;
using EcoTrack.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class luzController : ControllerBase
{
    public readonly IServicoLuz _servico;
    public luzController(IServicoLuz servico)
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
    public async Task<IActionResult> Post([FromBody] LuzCreateDto luz)
    {
        var result = await _servico.AdicionarLuz(luz);

        if (result.Dados == null)
        {
            return BadRequest(result.Mensagem);
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] LuzAtualizarDto luz)
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

    [HttpPost("CalcGasto/{id}")]
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

    [HttpPatch("Limite/{id}")]
    public async Task<IActionResult> AtualizarLimite(int id, [FromBody] double NovoLimite)
    {
        var result = await _servico.AtualizarLimiteLuz(id, NovoLimite);
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
        var result = await _servico.ZerarQuantidadeLuz(id);
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
        var result = await _servico.AtualizarQuantidadeLuz(id, novaQuantidade);
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
