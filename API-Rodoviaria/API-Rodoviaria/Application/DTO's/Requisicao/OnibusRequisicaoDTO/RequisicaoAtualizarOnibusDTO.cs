using System.ComponentModel.DataAnnotations;
namespace API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;

public class RequisicaoAtualizarOnibusDTO
{
    [Required, StringLength(10, MinimumLength =7)]
    public string Placa { get; set; } = string.Empty;
}
