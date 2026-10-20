using API_Rodoviaria.Domain.Interfaces.IPerfil;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Application.DTO_s.Resposta;

namespace API_Rodoviaria.Application.UseCase.PerfilCasosDeUso.CriarPerfil
{
    public class CriarPerfilCasoDeUso : ICriarPerfilCasoDeUso
    {
        private readonly IRepositorioPerfil _repositorioPerfil;

        public CriarPerfilCasoDeUso(IRepositorioPerfil repositorioPerfil)
        {
            _repositorioPerfil= repositorioPerfil;
        }

        public async Task<RespostaCriarPerfilDTO>
            ExecutarAsync(RequisicaoCriarPerfilDTO requisicao)
        {
            var nome = requisicao.Cargo.Trim();

            if (await _repositorioPerfil.ObterPorNomeAsync(nome) is not null)
                throw new ExcecaoDeNegocio("Já existe um perfil com esse nome.", 409);

            var perfil = new Perfil { Cargo = nome };
            await _repositorioPerfil.AdicionarAsync(perfil);

            return new RespostaCriarPerfilDTO { Id= perfil.Id, Cargo= perfil.Cargo };
        }

    }
}

