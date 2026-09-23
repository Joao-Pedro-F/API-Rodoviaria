namespace API_Rodoviaria.Domain.Models
{
    public class Rota
    {
        public int Id { get; set; }
        public string EnderecoInicio { get; set; } = string.Empty;
        public string EnderecoFim { get; set; } = string.Empty;

        public List<Viagem> Viagens { get; set; } = new();
    }
}
