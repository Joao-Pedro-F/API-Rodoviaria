using System.Net.Http.Headers;

namespace API_Rodoviaria.Domain.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime DataReserva { get; set; }
        public int FkUsuario { get; set; }
        public Usuario? Usuario { get; set; } = null!;  
        public int FkViagem { get; set; }
        public Viagem? Viagem { get; set; } = null!;
        public List<ReservaCadeira> ReservaCadeiras { get; set; } = new();
    }
}
