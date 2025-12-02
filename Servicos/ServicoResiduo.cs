using EcoTrack.Data;
using EcoTrack.Dto;
using EcoTrack.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.Servicos
{
    public class ServicoResiduo
    {
        private readonly DataContext _context;

        public ServicoResiduo(DataContext context)
        {
            _context = context;
        }

        public async Task<RetornoDto<Residuo>> ObterTodosResiduos()
        {
            List<Residuo> dados = await _context.Residuo.Include(c => c.Casa).OrderBy(d => d.Data).ToListAsync();

            return new RetornoDto<Residuo>
            {
                Mensagem = "Resíduos retornados com sucesso!",
                Dados = dados
            };
        }

        public async Task<RetornoDto<Residuo>> ObterResiduoPorId(int id)
        {
            List<Residuo> dados = new List<Residuo>();
            var residuo = await _context.Residuo.Include(c => c.Casa).FirstOrDefaultAsync(x => x.Id == id);

            if (residuo != null)
            {
                dados.Add(residuo);
                return new RetornoDto<Residuo>
                {
                    Mensagem = "Registro retornado com sucesso",
                    Dados = dados
                };
            }
            else
            {
                return new RetornoDto<Residuo>
                {
                    Mensagem = "Registro não encontrado",
                    Dados = null
                };
            }
        }

        public async Task<RetornoDto<Residuo>> AdicionarResiduo(Residuo residuo)
        {
            List<Residuo> dados = new List<Residuo>();

            if (residuo == null)
            {
                return new RetornoDto<Residuo>
                {
                    Mensagem = "Por favor envie um registro de água válido",
                    Dados = null
                };
            }

            try
            {
                _context.Residuo.Add(residuo);
                await _context.SaveChangesAsync();

                var created = await _context.Residuo.FirstOrDefaultAsync(a => a.Id == residuo.Id);
                dados.Add(created);

                return new RetornoDto<Residuo>
                {
                    Mensagem = "Água adicionada com sucesso",
                    Dados = dados
                };
            }
            catch (Exception ex)
            {
                return new RetornoDto<Residuo>
                {
                    Mensagem = $"Erro ao adicionar água: {ex.Message}",
                    Dados = null
                };
            }
        }

        public async Task<RetornoDto<Residuo>> AtualizarResiduo(Residuo residuoAtualizado)
        {
            List<Residuo> dados = new List<Residuo>();
            var residuo = await _context.Residuo.FirstOrDefaultAsync(x => x.Id == residuoAtualizado.Id);

            if (residuo != null)
            {
                return new RetornoDto<Residuo>
                {
                    Mensagem = "Registro não encontrado",
                    Dados = null
                };
            }
            else
            {
                _context.Entry(residuo).CurrentValues.SetValues(residuoAtualizado);

                await _context.SaveChangesAsync();

                var atualizacao = await _context.Residuo.Include(c => c.Casa).FirstOrDefaultAsync(t => t.Id == residuoAtualizado.Id);
                dados.Add(atualizacao);

                return new RetornoDto<Residuo>
                {
                    Mensagem = "Registro atualizado com sucesso",
                    Dados = dados
                };
            }
        }

        public async Task<RetornoDto<Residuo>> DeletarResiduo(int id)
        {
            List<Residuo> dados = new List<Residuo>();
            var residuo = await _context.Residuo.Include(c => c.Casa).FirstOrDefaultAsync(x => x.Id == id);
            if (residuo != null)
            {
                dados.Add(residuo);
                _context.Residuo.Remove(residuo);
                await _context.SaveChangesAsync();

                return new RetornoDto<Residuo>
                {
                    Mensagem = "Registro removido com sucesso",
                    Dados = dados
                };
            }
            else
            {
                return new RetornoDto<Residuo>
                {
                    Mensagem = "Registro não encontrado",
                    Dados = null
                };
            }
        }
    }
}