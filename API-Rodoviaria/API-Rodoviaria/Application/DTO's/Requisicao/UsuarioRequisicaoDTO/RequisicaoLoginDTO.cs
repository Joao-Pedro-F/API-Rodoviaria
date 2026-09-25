namespace API_Rodoviaria.Application.DTO_s.Requisicao.UsuarioRequisicaoDTO;
using System.ComponentModel.DataAnnotations;

    public class RequisicaoLoginDTO
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; } = string.Empty;
    }

