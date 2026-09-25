using API_Rodoviaria.Infrastructure.Security;

namespace API_Rodoviaria.Infrastructure.Services
{
    public class ServicoHashSenha : IServicoHashSenha
    {
        public string GerarhashSenha(string senha) => BCrypt.Net.BCrypt.HashPassword(senha);

        public bool VerificarSenha(string senha, string hash) => BCrypt.Net.BCrypt.Verify(senha, hash);

    }
}
