using EcoTrack.Dto;
using EcoTrack.Entidades;

namespace EcoTrack.Interfaces
{
    public interface IServicoResiduo
    {
        Task<RetornoDto<Residuo>> ObterTodosResiduos();
        Task<RetornoDto<Residuo>> ObterResiduoPorId(int id);
        Task<RetornoDto<Residuo>> AdicionarResiduo(Residuo residuo);
        Task<RetornoDto<Residuo>> AtualizarResiduo(Residuo residuoAtualizado);
        Task<RetornoDto<Residuo>> DeletarResiduo(int id);

        Task<RetornoDto<double>> AtualizarLimiteResiduo(int id, double NovoLimite);
        Task<RetornoDto<double>> ZerarQuantidadeResiduo(int id);
        Task<RetornoDto<double>> AtualizarQuantidadeResiduo(int id, double NovaQuantidade);
    }
}
