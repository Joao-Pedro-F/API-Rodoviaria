using System.Text.Json.Serialization;

namespace API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO
{
    public class RespostaLoginDTO
    {
        [JsonIgnore]
        public string Token { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
        public DateTime ExpiraEm { get; set; }
    }
}
