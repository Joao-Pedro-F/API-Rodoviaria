using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;
using API_Rodoviaria.Infrastructure.Security;

namespace API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.Login
{
    public class LoginCasodeUso : ILoginCasoDeUso
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IServicoHashSenha _hash;
        private readonly IServicoToken _servicoToken;

        public LoginCasodeUso(IRepositorioUsuario repositorioUsuario, IServicoHashSenha hash, IServicoToken servicoToken)
        {
            _repositorioUsuario = repositorioUsuario;
            _hash = hash;
            _servicoToken = servicoToken;
        }

        public async Task<RespostaLoginDTO> ExecutarAsync(RequisicaoLoginDTO requisicao)
        {

            var usuario = await _repositorioUsuario.ObterPorUsernameAsync(requisicao.Username);

            if (usuario is null || !_hash.VerificarSenha(requisicao.Password, usuario.Password))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            var token =_servicoToken.GerarToken(usuario);

            return new RespostaLoginDTO
            {
                Token = token.Token,
                Username = usuario.Username,
                Perfil = usuario.Perfil.Cargo,
                ExpiraEm = token.ExpiraEm
            };


        }
    }
}
