namespace API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO
{
    public class RespostaCriarUsuarioDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
        public string Endereço { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
