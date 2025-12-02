using EcoTrack.Data;
using EcoTrack.Dto;
using EcoTrack.Entidades;
using Microsoft.EntityFrameworkCore;
using EcoTrack.Interfaces;

namespace EcoTrack.Servicos
{
    public class ServicoAgua : IServicoAgua
    {
        private readonly DataContext _context;

        public ServicoAgua(DataContext context)
        {
            _context = context;
        }

        public async Task<RetornoDto<Agua>> AdicionarAgua(Agua agua)
        {
            List<Agua> dados = new List<Agua>();

            if (agua == null)
                return new RetornoDto<Agua>
                {
                    Mensagem = "Por favor envie um registro de água válido",
                    Dados = null
                };

            try
            {
                _context.Agua.Add(agua);
                await _context.SaveChangesAsync();

                var created = await _context.Agua.FirstOrDefaultAsync(a => a.Id == agua.Id);
                dados.Add(created);

                return new RetornoDto<Agua>
                {
                    Mensagem = "Água adicionada com sucesso",
                    Dados = dados
                };
            }
            catch (Exception ex)
            {
                return new RetornoDto<Agua>
                {
                    Mensagem = $"Erro ao adicionar água: {ex.Message}",
                    Dados = null
                };
            }
        }

        public async Task<RetornoDto<Agua>> ObterTodasAguas()
        {
            List<Agua> dados = await _context.Agua.Include(c => c.Casa).OrderBy(d => d.Data).ToListAsync();

            return new RetornoDto<Agua>
            {
                Mensagem = "Registros de água retornados com sucesso",
                Dados = dados
            };
        }

        public async Task<RetornoDto<Agua>> ObterAguaPorId(int id)
        {
            List<Agua> dados = new List<Agua>();
            var agua = await _context.Agua.Include(c => c.Casa).FirstOrDefaultAsync(x => x.Id == id);
            if (agua != null)
            {
                dados.Add(agua);
                return new RetornoDto<Agua>
                {
                    Mensagem = "Registro retornado com sucesso",
                    Dados = dados
                };
            }
            else
            {
                return new RetornoDto<Agua>
                {
                    Mensagem = "Registro não encontrado",
                    Dados = null
                };
            }
        }

        public async Task<RetornoDto<Agua>> RemoverAgua(int id)
        {
            List<Agua> dados = new List<Agua>();
            var agua = await _context.Agua.Include(c => c.Casa).FirstOrDefaultAsync(x => x.Id == id);
            if (agua != null)
            {
                dados.Add(agua);
                _context.Agua.Remove(agua);
                await _context.SaveChangesAsync();

                return new RetornoDto<Agua>
                {
                    Mensagem = "Registro removido com sucesso",
                    Dados = dados
                };
            }
            else
            {
                return new RetornoDto<Agua>
                {
                    Mensagem = "Registro não encontrado",
                    Dados = null
                };
            }
        }

        public async Task<RetornoDto<Agua>> AtualizarAgua(Agua aguaAtualizada)
        {
            List<Agua> dados = new List<Agua>();
            var agua = await _context.Agua.FirstOrDefaultAsync(x => x.Id == aguaAtualizada.Id);
            if (agua != null)
            {
                _context.Entry(agua).CurrentValues.SetValues(aguaAtualizada);

                await _context.SaveChangesAsync();

                var updated = await _context.Agua.Include(c => c.Casa).FirstOrDefaultAsync(t => t.Id == aguaAtualizada.Id);
                dados.Add(updated);
                return new RetornoDto<Agua>
                {
                    Mensagem = "Registro atualizado com sucesso",
                    Dados = dados
                };
            }
            else
            {
                return new RetornoDto<Agua>
                {
                    Mensagem = "Registro não encontrado",
                    Dados = null
                };
            }
        }

        public async Task<RetornoDto<double>> CalcularGastoAgua(int id, double valor)
        {
            if (valor <= 0)
            {
                return new RetornoDto<double>
                {
                    Mensagem = "O valor deve ser maior que zero.",
                    Dados = null
                };
            }
            var agua = await _context.Agua.FirstOrDefaultAsync(x => x.Id == id);
            if (agua != null)
            {
                return new RetornoDto<double>
                {
                    Mensagem = "Cálculo realizado com sucesso.",
                    Dados = new List<double> { agua.Quantidade * valor }
                };

            }
            return new RetornoDto<double>
            {
                Mensagem = "Registro de água não encontrado.",
                Dados = null
            };
        }


        public async Task<RetornoDto<double>> AtualizarLimiteAgua(int id, double NovoLimite)
        {
            var agua = await _context.Agua.FirstOrDefaultAsync(x => x.Id == id);
            if (agua != null)
            {
                if (NovoLimite < agua.Quantidade)
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
                agua.Limite = NovoLimite;
                await _context.SaveChangesAsync();
                return new RetornoDto<double>
                {
                    Mensagem = "Limite atualizado com sucesso.",
                    Dados = new List<double> { agua.Limite }
                };
            }
            return new RetornoDto<double>
            {
                Mensagem = "Registro de agua não encontrado.",
                Dados = null
            };
        }
        public async Task<RetornoDto<double>> ZerarQuantidadeAgua(int id)
        {
            var agua = await _context.Agua.FirstOrDefaultAsync(x => x.Id == id);
            if (agua != null)
            {
                agua.Quantidade = 0;
                await _context.SaveChangesAsync();
                return new RetornoDto<double>
                {
                    Mensagem = "Quantidade zerada com sucesso.",
                    Dados = new List<double> { agua.Quantidade }
                };
            }
            return new RetornoDto<double>
            {
                Mensagem = "Registro de agua não encontrado.",
                Dados = null
            };
        }
        public async Task<RetornoDto<double>> AtualizarQuantidadeAgua(int id, double NovaQuantidade)
        {
            var result = await _context.Agua.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                var mensagem = "";
                if (result.Quantidade+NovaQuantidade > result.Limite)
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
                Mensagem = "Registro de agua não encontrado.",
                Dados = null
            };
        }
    }
}
