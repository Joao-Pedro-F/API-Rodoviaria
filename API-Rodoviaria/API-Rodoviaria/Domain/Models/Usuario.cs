namespace API_Rodoviaria.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }

        public int FkPerfil { get; set; }
        public Perfil Perfil { get; set; } = null!;

        public List<Reserva> Reservas { get; set; } = new();
    }
}
