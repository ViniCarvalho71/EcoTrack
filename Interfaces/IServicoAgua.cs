using EcoTrack.Dto;
using EcoTrack.Entidades;

namespace EcoTrack.Interfaces
{
    public interface IServicoAgua
    {
        Task<RetornoDto<Agua>> AdicionarAgua(Agua agua);
        Task<RetornoDto<Agua>> ObterTodasAguas();
        Task<RetornoDto<Agua>> ObterAguaPorId(int id);
        Task<RetornoDto<Agua>> RemoverAgua(int id);
        Task<RetornoDto<Agua>> AtualizarAgua(Agua aguaAtualizada);

        Task<RetornoDto<double>> CalcularGastoAgua(int id, double valor);
        Task<RetornoDto<double>> AtualizarLimiteAgua(int id, double NovoLimite);
        Task<RetornoDto<double>> ZerarQuantidadeAgua(int id);
        Task<RetornoDto<double>> AtualizarQuantidadeAgua(int id, double NovaQuantidade);
    }
}
