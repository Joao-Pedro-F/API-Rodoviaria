namespace API_Rodoviaria.Infrastructure.Security
{
    public interface IServicoHashSenha
    {
        string GerarhashSenha(string senha);
        bool VerificarSenha(string senha, string hash);
    }
}
