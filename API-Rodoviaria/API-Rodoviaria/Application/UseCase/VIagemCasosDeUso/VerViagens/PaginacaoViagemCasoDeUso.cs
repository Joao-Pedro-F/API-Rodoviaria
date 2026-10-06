using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Application.DTO_s.Resposta;
using API_Rodoviaria.Application.DTO_s.Requisicao;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
using API_Rodoviaria.Application.DTO_s.Resposta.PaginacaoRespostaDTO;
using API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO;
namespace API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.VerViagens;

public class PaginacaoViagemCasoDeUso : IPaginacaoViagemCasoDeUso
{
    private readonly IRepositorioViagem _repositorioViagem;

    public PaginacaoViagemCasoDeUso(IRepositorioViagem repositorioViagem)
    {
        _repositorioViagem = repositorioViagem;
    }
    public async Task<PaginacaoResposta<RespostaCriarViagemDTO>> ExecutarAsync(PaginacaoRequisicao requisicao)
    {
        var (itens, total) = await _repositorioViagem.ListarPaginadoAsync(requisicao.Pagina, requisicao.TamanhoPagina);

        return new PaginacaoResposta<RespostaCriarViagemDTO>
        {
            Itens = itens.Select(v => new RespostaCriarViagemDTO
            {
                Id = v.Id,
                FkOnibus = v.FkOnibus,
                Placa = v.Onibus.Placa,
                FkMotorista = v.FkMotorista,
                NomeMotorista = v.Motorista.Nome,
                FkRota = v.FkRota,
                EnderecoOrigem = v.Rota.EnderecoInicio,
                EnderecoFim = v.Rota.EnderecoFim,
                DataSaida = v.DataSaida,
                DataChegada = v.DataChegada,
            }).ToList(),
            PaginaAtual = requisicao.Pagina,
            TamanhoPagina = requisicao.TamanhoPagina,
            TotalItens = total
        };
    }
}
