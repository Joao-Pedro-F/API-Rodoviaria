namespace API_Rodoviaria.Domain.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Cargo { get; set; } = string.Empty;

        public List<Usuario> Usuarios { get; set; } = new();  
    }
}
