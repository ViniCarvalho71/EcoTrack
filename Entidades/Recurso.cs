namespace EcoTrack.Entidades
{
    public abstract class Recurso
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        public double Quantidade { get; set; } //kg, litros, etc...

        public abstract string TipoRecurso { get; }
    }
}
