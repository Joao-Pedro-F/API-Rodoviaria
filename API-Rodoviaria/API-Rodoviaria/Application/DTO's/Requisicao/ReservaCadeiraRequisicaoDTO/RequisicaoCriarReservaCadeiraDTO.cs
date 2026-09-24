namespace API_Rodoviaria.Application.DTO_s.Requisicao.ReservaCadeiraRequisicaoDTO;
using System.ComponentModel.DataAnnotations;

public class RequisicaoCriarReservaCadeiraDTO
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public int FkCadeira { get; set; } = 0;
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public int FkReserva { get; set; } = 0;
}
