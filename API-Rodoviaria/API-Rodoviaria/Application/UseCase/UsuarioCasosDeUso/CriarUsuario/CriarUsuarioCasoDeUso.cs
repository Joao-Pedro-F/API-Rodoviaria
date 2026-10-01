using API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;
using API_Rodoviaria.Infrastructure.Security;

namespace API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.CriarUsuario
{
    public class CriarUsuarioCasoDeUso : ICriarUsuarioCasoDeUso
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioPerfil _repositorioPerfil;
        private readonly IServicoHashSenha _hash;
        public CriarUsuarioCasoDeUso(
        IRepositorioUsuario repositorioUsuario,
        IRepositorioPerfil repositorioPerfil,
        IServicoHashSenha hash)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPerfil = repositorioPerfil;
            _hash = hash;
        }
        public async Task<RespostaCriarUsuarioDTO>
        ExecutarAsync(RequisicaoCriarUsuarioDTO requisicao)
        {
            if (await _repositorioUsuario.ExisteAsync(requisicao.Username,
            requisicao.Email, requisicao.Cpf))
                throw new ExcecaoDeNegocio("Já existe um usuário com esse username, e - mail ou CPF.", 409);
                var perfilCliente = await
                _repositorioPerfil.ObterPorNomeAsync(NomesPerfis.Cliente)
                ?? throw new ExcecaoDeNegocio("Perfil 'Cliente' não encontrado. Verifique a semente inicial.", 500);

            var usuario = new Usuario
            {
                Username = requisicao.Username.Trim(),
                Password = _hash.GerarhashSenha(requisicao.Password),
                Email = requisicao.Email.Trim(),
                Cpf = requisicao.Cpf,
                Endereco = requisicao.Endereco.Trim(),
                FkPerfil = perfilCliente.Id
            };
            await _repositorioUsuario.AdicionarAsync(usuario);
            return new RespostaCriarUsuarioDTO
            {
                Id= usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Perfil = perfilCliente.Cargo
            };
        }
    }
}
