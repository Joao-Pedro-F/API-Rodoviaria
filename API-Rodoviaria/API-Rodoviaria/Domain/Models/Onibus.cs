namespace API_Rodoviaria.Domain.Models
{
    public class Onibus
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public int CapacidadeTotal { get; set; }
       
        public List<Viagem> Viagens { get; set; } = new();
        public List<Cadeira> Cadeiras { get; set; } = new();
    }
}
