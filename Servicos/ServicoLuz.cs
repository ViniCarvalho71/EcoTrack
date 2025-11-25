using EcoTrack.Data;
using EcoTrack.Dto;
using EcoTrack.Entidades;
using Microsoft.EntityFrameworkCore;


namespace EcoTrack.Servicos;

public class ServicoLuz
{
    private readonly DataContext _context;
    public ServicoLuz(DataContext context)
    {
        _context = context;
    }
        
    public async Task<RetornoDto<Luz>> ObterTodasLuz()
    {
        List<Luz> dados = await _context.Luz.Include(c => c.Casa).ToListAsync();
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
                return new RetornoDto<Luz>
                {
                    Mensagem = "Registro de luz adicionado com sucesso",
                    Dados = new List<Luz> { novaLuz }
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
        var luz = _context.Luz.FirstOrDefaultAsync(x => x.Id == luzAtualizada.Id);
        if (luz == null)
        {
            return new RetornoDto<Luz>
            {
                Mensagem = "Registro de luz não encontrado",
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

    }
