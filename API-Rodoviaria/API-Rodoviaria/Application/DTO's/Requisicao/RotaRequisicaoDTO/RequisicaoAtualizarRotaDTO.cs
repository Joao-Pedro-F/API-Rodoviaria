using System.ComponentModel.DataAnnotations;
namespace API_Rodoviaria.Application.DTO_s.Requisicao.RotaRequisicaoDTO;

public class RequisicaoAtualizarRotaDTO
{
    [Required, StringLength(200)]
    public string EnderecoInicial { get; set; } = string.Empty;
    [Required, StringLength(200)]
    public string EnderecoFinal { get; set; } = string.Empty;
}
