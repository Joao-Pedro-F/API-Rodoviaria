using System.ComponentModel.DataAnnotations;
namespace API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;

public class RequisicaoAtualizarUsuarioDTO
{
    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;
    [Required, StringLength(200)]
    public string Endereco { get; set; } = string.Empty;
}
