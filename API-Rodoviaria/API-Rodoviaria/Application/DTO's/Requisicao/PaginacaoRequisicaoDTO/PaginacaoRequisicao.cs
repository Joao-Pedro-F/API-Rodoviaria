using System.ComponentModel.DataAnnotations;
namespace API_Rodoviaria.Application.DTO_s.Requisicao.PaginacaoRequisicaoDTO;

public class PaginacaoRequisicao
{
    [Range(1, int.MaxValue, ErrorMessage = "A página deve ser maior ou igual a 1.")]
    public int Pagina { get; set; } = 1;
    [Range(1, 100, ErrorMessage = "O tamanho da página deve ser maior ou igual a 1.")]
    public int TamanhoPagina { get; set; } = 10;
}
