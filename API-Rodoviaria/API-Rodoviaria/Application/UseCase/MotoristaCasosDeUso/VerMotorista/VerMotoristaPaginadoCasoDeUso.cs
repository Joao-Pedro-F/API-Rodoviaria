using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.UseCase.CadeiraCasosDeUso.ListarCadeira;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
namespace API_Rodoviaria.Application.UseCase.MotoristaCasosDeUso.VerMotorista;

public class VerMotoristaPaginadoCasoDeUso //: IVerMotoristaPaginadoCasoDeUso
{
    private readonly IRepositorioMotorista _motoristaRepositorio;

    public VerMotoristaPaginadoCasoDeUso(IRepositorioMotorista motoristaRepositorio)
    {
        _motoristaRepositorio = motoristaRepositorio;
    }
    public async Task<PaginacaoResposta<RespostaCriarMotoristaDTO>> ExecutarAsync(PaginacaoRequisicao paginacao)
    {
        var (itens, total) = await _motoristaRepositorio.ListarPaginadoAsync(paginacao.Pagina, paginacao.TamanhoPagina);
        return new PaginacaoResposta<RespostaCriarMotoristaDTO>
        {
            Itens = itens.Select(m => new RespostaCriarMotoristaDTO
            {
                Id = m.Id,
                Nome = m.Nome,
                Cpf = m.Cpf,
                Cnh = m.Cnh
            }).ToList(),
            PaginaAtual = paginacao.Pagina,
            TamanhoPagina = paginacao.TamanhoPagina,
            TotalItens = total,
        };
    }
}
