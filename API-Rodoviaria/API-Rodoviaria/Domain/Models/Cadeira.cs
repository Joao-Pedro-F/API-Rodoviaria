namespace API_Rodoviaria.Domain.Models
{
    public class Cadeira
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int FkOnibus { get; set; }

        public bool Disponivel { get; set; } = true;
        public Onibus? Onibus { get; set; } = null!;

        public List<ReservaCadeira> ReservaCadeira { get; set; } = new();
    }
}
