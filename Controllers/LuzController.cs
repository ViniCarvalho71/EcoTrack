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
        if (result != null)
        {
            return Ok(result);

        }
        else
        {
            return NotFound(result.Mensagem);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Luz luz)
    {
        if (luz == null)
        {
            return BadRequest("Dados de luz inválidos");
        }
        var result = await _servico.AdicionarLuz(luz);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Luz luz, int id) // Terminar isso
    {
        if (luz == null) {
            return BadRequest("Dados de luz inválidos");
        }
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)// Terminar isso
    {
        return Ok();
    }
}
