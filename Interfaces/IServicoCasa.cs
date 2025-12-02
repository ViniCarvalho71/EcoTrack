using EcoTrack.Dto;
using EcoTrack.Entidades;

namespace EcoTrack.Interfaces
{
    public interface IServicoCasa
    {
        Task<RetornoDto<Casa>> AdicionarCasa(Casa casa);
        Task<RetornoDto<Casa>> ObterTodasCasas();
        Task<RetornoDto<Casa>> ObterCasaPorId(int id);
        Task<RetornoDto<Casa>> RemoverCasa(int id);
        Task<RetornoDto<Casa>> AtualizarCasa(Casa casaAtualizada);
    }
}
