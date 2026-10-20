using System.ComponentModel.DataAnnotations;

namespace API_Rodoviaria.Application.DTO_s.Requisicao.CadeiraRequisicaoDTO;


public class RequisicaoCriarCadeiraDTO
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string Numero { get; set; } = null!;
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public int FkOnibus { get; set; } = 0;
    

}
