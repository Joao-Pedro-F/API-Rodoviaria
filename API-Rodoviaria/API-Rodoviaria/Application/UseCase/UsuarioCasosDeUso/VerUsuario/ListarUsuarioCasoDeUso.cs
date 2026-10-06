using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IUsuario;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;

namespace API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.VerUsuario
{
    public class ListarUsuarioCasoDeUso : IListarUsuarioCasoDeUso
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
  

    public ListarUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }
    
       public async Task<RespostaCriarUsuarioDTO> ExecutarAsync(int id)
        {
            var usuario = await _repositorioUsuario.ObterPorIdAsync(id) ?? throw new KeyNotFoundException("Usuário não encontrado.");

            return new RespostaCriarUsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Endereço = usuario.Endereco,
                Perfil = usuario.Perfil.Cargo,
            };
        }
    } 
}
