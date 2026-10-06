using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.UsuarioRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IUsuario;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;

namespace API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.VerUsuario
{
    public class PaginacaoUsuarioCasoDeUso : IPaginacaoUsuarioCasoDeUso
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
    
        public PaginacaoUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public async Task<PaginacaoResposta<RespostaCriarUsuarioDTO>> ExecutarAsync(PaginacaoRequisicao paginacao)
        {
            var (itens, total) = await _repositorioUsuario.ListarPaginadoAsync(paginacao.Pagina, paginacao.TamanhoPagina);
            return new PaginacaoResposta<RespostaCriarUsuarioDTO>
            {
                Itens = itens.Select(u => new RespostaCriarUsuarioDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Endereço = u.Endereco,
                    Perfil = u.Perfil.Cargo


                }).ToList(),
                PaginaAtual=paginacao.Pagina,
                TamanhoPagina=paginacao.TamanhoPagina,
                TotalItens=total
            };
        }
    }
}
