using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IUsuario;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;

namespace API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.AtualizarUsuario
{
    public class AtualizarUsuarioCasoDeUso : IAtualizarUsuarioCasoDeUso
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public AtualizarUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public async Task<RespostaCriarUsuarioDTO> ExecutarAsync(int id, RequisicaoAtualizarUsuarioDTO requisicao)
        {
            var usuario = await _repositorioUsuario.ObterPorIdAsync(id)?? throw new KeyNotFoundException("Usuário não encontrado");

            usuario.Email = requisicao.Email.Trim();
            usuario.Endereco = requisicao.Endereco.Trim();

            await _repositorioUsuario.AtualizarAsync(usuario);

            return new RespostaCriarUsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Perfil= usuario.Perfil.Cargo
            };
        }
    }
}
