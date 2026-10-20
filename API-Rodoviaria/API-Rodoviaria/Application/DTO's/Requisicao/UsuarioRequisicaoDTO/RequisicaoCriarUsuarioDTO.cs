namespace API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using System.ComponentModel.DataAnnotations;

public class RequisicaoCriarUsuarioDTO
{
    [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "O email do usuário é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha do usuário é obrigatória.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF do usuário é obrigatório.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O endereço do usuário é obrigatório.")]
    public string Endereco { get; set; }
}
