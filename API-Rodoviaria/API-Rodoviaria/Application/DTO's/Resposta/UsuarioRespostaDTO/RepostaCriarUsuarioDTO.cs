namespace API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO
{
    public class RepostaCriarUsuarioDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
    }
}
