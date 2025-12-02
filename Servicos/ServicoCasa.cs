using EcoTrack.Data;
using EcoTrack.Dto;
using EcoTrack.Entidades;
using Microsoft.EntityFrameworkCore;
using EcoTrack.Interfaces;

namespace EcoTrack.Servicos
{
    public class ServicoCasa : IServicoCasa
    {
        private readonly DataContext _context;

        public ServicoCasa(DataContext context)
        {
            _context = context;
        }

        // Métodos para gerenciar casas podem ser adicionados aqui

        public async Task<RetornoDto<Casa>> AdicionarCasa(Casa casa)
        {
            List<Casa> dados = new List<Casa>();

            if (casa == null)
                return new RetornoDto<Casa>
                {
                    Mensagem = "Por favor envie uma casa válida",
                    Dados = null
                };

            try
            {
                _context.Casa.Add(casa);
                await _context.SaveChangesAsync();

                var createdCasa = await _context.Casa.FirstOrDefaultAsync(c => c.Id == casa.Id); 
                dados.Add(createdCasa);

                return new RetornoDto<Casa>
                {
                    Mensagem = "Casa adicionada com sucesso",
                    Dados = dados
                };
            }
            catch (Exception ex)
            {
                return new RetornoDto<Casa>
                {
                    Mensagem = $"Erro ao adicionar casa: {ex.Message}",
                    Dados = null
                };

            }
        }

        public async Task<RetornoDto<Casa>> ObterTodasCasas()
        {
            List<Casa> dados = await _context.Casa.ToListAsync();

            return new RetornoDto<Casa>
            {
                Mensagem = "Casas retornadas com sucesso",
                Dados = dados
            };
        }

        public async Task<RetornoDto<Casa>> ObterCasaPorId(int id)
        {
            List<Casa> dados = new List<Casa>();
            var casa = await _context.Casa.FindAsync(id);
            if (casa != null)
            {
                dados.Add(casa);
                return new RetornoDto<Casa>
                {
                    Mensagem = "Casa retornada com sucesso",
                    Dados = dados
                };
            }
            else
            {
                return new RetornoDto<Casa>
                {
                    Mensagem = "Casa não encontrada",
                    Dados = null
                };
            }
        }

        public async Task<RetornoDto<Casa>> RemoverCasa(int id)
        {
            List<Casa> dados = new List<Casa>();
            var casa = await _context.Casa.FindAsync(id);
            if (casa != null)
            {
                dados.Add(casa);
                _context.Casa.Remove(casa);
                await _context.SaveChangesAsync();

                return new RetornoDto<Casa>
                {
                    Mensagem = "Casa removida com sucesso",
                    Dados = dados
                };
            }
            else
            {
                return new RetornoDto<Casa>
                {
                    Mensagem = "Casa não encontrada",
                    Dados = null
                };
            }
        }

        public async Task<RetornoDto<Casa>> AtualizarCasa(Casa casaAtualizada)
        {
            List<Casa> dados = new List<Casa>();
            var casa = await _context.Casa.FindAsync(casaAtualizada.Id);
            if (casa != null)
            {
                _context.Entry(casa).CurrentValues.SetValues(casaAtualizada);

                await _context.SaveChangesAsync();

                var updated = await _context.Casa.FirstOrDefaultAsync(t => t.Id == casaAtualizada.Id);
                dados.Add(updated);
                return new RetornoDto<Casa>
                {
                    Mensagem = "Casa atualizada com sucesso",
                    Dados = dados
                };
            }
            else
            {
                return new RetornoDto<Casa>
                {
                    Mensagem = "Casa não encontrada",
                    Dados = null
                };
            }
        }
    }
}
