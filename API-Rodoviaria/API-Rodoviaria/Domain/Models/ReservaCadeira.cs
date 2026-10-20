namespace API_Rodoviaria.Domain.Models
{
    public class ReservaCadeira
    {
        public int Id { get; set; }
        public int FkCadeira { get; set; }
        public Cadeira? Cadeira { get; set; } = null!;  
        public int FkReserva { get; set; }
        public Reserva? Reserva { get; set; }
    }
}
