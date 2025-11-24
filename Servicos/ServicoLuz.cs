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
        else
        {
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
    }
}