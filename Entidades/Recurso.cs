using System.ComponentModel.DataAnnotations;

namespace EcoTrack.Entidades
{
    public abstract class Recurso
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        [Required]
        public double Quantidade { get; set; } //kg, litros, etc...
        [Required]
        public int CasaId{ get; set; }
        public Casa? Casa { get; set; }

        public abstract string TipoRecurso { get; }


    }
}
