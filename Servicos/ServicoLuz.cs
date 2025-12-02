using EcoTrack.Data;
using EcoTrack.Dto;
using EcoTrack.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using EcoTrack.Interfaces;


namespace EcoTrack.Servicos;

public class ServicoLuz : IServicoLuz
{
    private readonly DataContext _context;
    public ServicoLuz(DataContext context)
    {
        _context = context;
    }
        
    public async Task<RetornoDto<Luz>> ObterTodasLuz()
    {
        List<Luz> dados = await _context.Luz.Include(c => c.Casa).OrderBy(d => d.Data).ToListAsync();
            return new RetornoDto<Luz>
            {
                Mensagem = "Registros de luz retornados com sucesso",
                Dados = dados
            };
    }
    public async Task<RetornoDto<Luz>> ObterLuzPorId(int id)
    {
        List<Luz> dados = new List<Luz>();
        var luz = await _context.Luz.Include(c => c.Casa).FirstOrDefaultAsync(x => x.Id == id);
        if (luz != null)
        {
            dados.Add(luz);
            return new RetornoDto<Luz>
            {
                Mensagem = "Registros de luz retornados com sucesso",
                Dados = dados
            };
        }
        else
        {
            return new RetornoDto<Luz>
            {
                Mensagem = "Registro de luz não encontrado",
                Dados = null
            };
        }

    }

    public async Task<RetornoDto<Luz>> AdicionarLuz(Luz novaLuz)
    {
        List<Luz> dados = new List<Luz>();
        if (novaLuz == null)
        {
            return new RetornoDto<Luz>
            {
                Mensagem = "Dados de luz inválidos",
                Dados = null
            };
        }
        
        try
        {
            _context.Luz.Add(novaLuz);
            await _context.SaveChangesAsync();
            var result = await _context.Luz.FirstOrDefaultAsync(a => a.Id == novaLuz.Id);
            dados.Add(result);
            return new RetornoDto<Luz>

            {
                Mensagem = "Registro de luz adicionado com sucesso",
                Dados = dados
            };
            
        }
        catch (Exception ex)
        {
            return new RetornoDto<Luz>
            {
                Mensagem = $"Erro ao adicionar registro de luz: {ex.Message}",
                Dados = null
            };
        }
    }
        
    public async Task<RetornoDto<Luz>> AtualizarLuz(Luz luzAtualizada)
    {
        List<Luz> dados = new List<Luz>();
        var luz = await _context.Luz.FirstOrDefaultAsync(x => x.Id == luzAtualizada.Id);
        if (luz != null)
        {
            if(luz.Limite > luzAtualizada.Quantidade)
            {
                return new RetornoDto<Luz>
                {
                    Mensagem = "A quantidade de luz consumida não pode ser menor que o limite estabelecido.",
                    Dados = null
                };
            }
            _context.Entry(luz).CurrentValues.SetValues(luzAtualizada);
            await _context.SaveChangesAsync();
            var atualizado = await _context.Luz.Include(c => c.Casa).FirstOrDefaultAsync(x => x.Id == luzAtualizada.Id);
            dados.Add(atualizado);
            return new RetornoDto<Luz>
            {
                Mensagem = "Registro de luz atualizado com sucesso",
                Dados = dados
            };
        }
        
        return new RetornoDto<Luz>
        {
            Mensagem = "Registro de luz não encontrado",
            Dados = null
        };
    }

    public async Task<RetornoDto<Luz>> DeletarLuz(int id)
    {
        List<Luz> dados = new List<Luz>();
        var result = await _context.Luz.Include(c=>c.Casa).FirstOrDefaultAsync(x=> x.Id == id);
        if (result == null)
        {
            return new RetornoDto<Luz>
            {
                Mensagem = "Registro de luz não encontrado",
                Dados = null
            };

        }
        dados.Add(result);
        _context.Luz.Remove(result);
        await _context.SaveChangesAsync();
        return new RetornoDto<Luz>
        { Mensagem = "Luz removida com sucesso",
          Dados = dados
        };
    }
    public async Task<RetornoDto<double>> CalcularGastoLuz(int id, double valor)
    {
        if (valor <= 0)
        {
            return new RetornoDto<double>
            {
                Mensagem = "O valor deve ser maior que zero.",
                Dados = null
            };
        }
        var luz = await _context.Luz.FirstOrDefaultAsync(x => x.Id == id);
        if (luz != null)
        {
            return new RetornoDto<double>
            {
                Mensagem = "Cálculo realizado com sucesso.",
                Dados = new List<double> { luz.Quantidade * valor }
            };

        }
        return new RetornoDto<double>
        {
            Mensagem = "Registro de água não encontrado.",
            Dados = null
        };
    }

    public async Task<RetornoDto<double>> AtualizarLimiteLuz(int id, double NovoLimite)
    {
        var luz =  await _context.Luz.FirstOrDefaultAsync(x => x.Id == id);
        if (luz != null)
        {
            if (NovoLimite < luz.Quantidade)
            {
                return new RetornoDto<double>
                {
                    Mensagem = "O novo limite não pode ser menor que a quantidade já consumida.",
                    Dados = null
                };
            }
            if (NovoLimite < 0)
            {
                return new RetornoDto<double>
                {
                    Mensagem = "O novo limite não pode ser negativo.",
                    Dados = null
                };
            }
            luz.Limite = NovoLimite;
            await _context.SaveChangesAsync();
            return new RetornoDto<double>
            {
                Mensagem = "Limite atualizado com sucesso.",
                Dados = new List<double> { luz.Limite }
            };
        }
        return new RetornoDto<double>
        {
            Mensagem = "Registro de luz não encontrado.",
            Dados = null
        };
    }
    public async Task<RetornoDto<double>> ZerarQuantidadeLuz(int id) 
    { var luz = await _context.Luz.FirstOrDefaultAsync(x => x.Id == id);
        if (luz != null)
        {
            luz.Quantidade = 0;
            await _context.SaveChangesAsync();
            return new RetornoDto<double>
            {
                Mensagem = "Quantidade zerada com sucesso.",
                Dados = new List<double> { luz.Quantidade }
            };
        }
        return new RetornoDto<double>
        {
            Mensagem = "Registro de luz não encontrado.",
            Dados = null
        };
    }

    public async Task<RetornoDto<double>> AtualizarQuantidadeLuz(int id, double NovaQuantidade)
    {
        var result = await _context.Luz.FirstOrDefaultAsync(x => x.Id == id);
        if (result != null)
        {
            var mensagem = "";
            if (result.Quantidade + NovaQuantidade > result.Limite)
            {
                mensagem = "ALERTA! O limite definido foi ultrapassado!";
            }
            if (NovaQuantidade < 0)
            {
                return new RetornoDto<double>
                {
                    Mensagem = "A quantidade não pode ser negativa.",
                    Dados = null
                };
            }
            result.Quantidade = result.Quantidade+NovaQuantidade;
            await _context.SaveChangesAsync();
            return new RetornoDto<double>
            {
                Mensagem = "Quantidade atualizada com sucesso."+mensagem,
                Dados = new List<double> { result.Quantidade }
            };
        }
        return new RetornoDto<double>
        {
            Mensagem = "Registro de luz não encontrado.",
            Dados = null
        };
    }

}
