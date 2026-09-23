namespace API_Rodoviaria.Domain.Models
{
    public class Viagem
    {
        public int Id { get; set; }
        public DateTime DataSaida { get; set; }
        public DateTime DataChegada { get; set; }

        public int FkOnibus { get; set; }
        public Onibus? Onibus { get; set; }  = null!;

        public int FkMotorista { get; set; }
        public Motorista? Motorista { get; set; } = null!;

        public int FkRota { get; set; }
        public Rota? Rota { get; set; } = null!;

        public List<Reserva> Reservas { get; set; } = new();
    }
}
