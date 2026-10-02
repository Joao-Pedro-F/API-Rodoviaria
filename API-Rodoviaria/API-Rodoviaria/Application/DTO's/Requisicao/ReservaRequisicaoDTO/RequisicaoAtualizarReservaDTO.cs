using System.ComponentModel.DataAnnotations;
namespace API_Rodoviaria.Application.DTO_s.Requisicao.ReservaRequisicaoDTO;

public class RequisicaoAtualizarReservaDTO
{
    [Required, MinLength(1, ErrorMessage ="Escolha pelo menos 1 cadeira.")]
    public List<int> IdsCadeiras { get; set; } = new();
}
