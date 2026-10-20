namespace API_Rodoviaria.Domain.Models
{
    public class Motorista
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Cnh { get; set; } = string.Empty;
      
        public List<Viagem> Viagens { get; set; } = new();
    }
}
