namespace API_Rodoviaria.Application.DTO_s.Requisicao.MotoristaRequisicaoDTO;
using System.ComponentModel.DataAnnotations;

    public class RequisicaoCriarMotoristaDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [StringLength(11, ErrorMessage = "O campo CPF deve ter no máximo 11 caracteres.")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo CNH é obrigatório.")]
        [StringLength(11, ErrorMessage = "O campo CNH deve ter no máximo 11 caracteres.")]
        public string Cnh { get; set; }
        
    }

