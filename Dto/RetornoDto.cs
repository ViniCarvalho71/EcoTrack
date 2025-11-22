namespace EcoTrack.Dto
{
    public class RetornoDto<T>
    {
        public string Mensagem { get; set; }
        public List<T> Dados { get; set; }
    }
}
