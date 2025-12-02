using EcoTrack.Dto;
using EcoTrack.Entidades;

namespace EcoTrack.Interfaces
{
    public interface IServicoLuz
    {
        Task<RetornoDto<Luz>> ObterTodasLuz();
        Task<RetornoDto<Luz>> ObterLuzPorId(int id);
        Task<RetornoDto<Luz>> AdicionarLuz(Luz novaLuz);
        Task<RetornoDto<Luz>> AtualizarLuz(Luz luzAtualizada);
        Task<RetornoDto<Luz>> DeletarLuz(int id);

        Task<RetornoDto<double>> CalcularGastoLuz(int id, double valor);
        Task<RetornoDto<double>> AtualizarLimiteLuz(int id, double NovoLimite);
        Task<RetornoDto<double>> ZerarQuantidadeLuz(int id);
        Task<RetornoDto<double>> AtualizarQuantidadeLuz(int id, double NovaQuantidade); 
    }
}
