using System.ComponentModel.DataAnnotations;
namespace API_Rodoviaria.Application.DTO_s.Requisicao.MotoristaRequisicaoDTO;

public class RequisicaoAtualizarMotoristaDTO
{
    [Required, StringLength(100, MinimumLength =3)]
    public string Nome { get; set; } = string.Empty;
    [Required, RegularExpression(@"^[0-9]{3}\.[0-9]{3}\.[0-9]{3}\-[0-9]{2}$", ErrorMessage = "CPF inválido.")]
    public string Cpf { get; set; } = string.Empty;
    [Required, RegularExpression(@"^[0-9]{11}$", ErrorMessage = "CNH inválida.")]
    public string? Cnh { get; set; } = string.Empty;
}
