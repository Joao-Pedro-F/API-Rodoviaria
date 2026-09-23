namespace API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
using System.ComponentModel.DataAnnotations;

public class RequisicaoCriarOnibusDTO
{
    [Required(ErrorMessage = "O campo Placa é obrigatório.")]
    [StringLength(7, ErrorMessage = "O campo Placa deve ter no máximo 7 caracteres.")]
    public string Placa { get; set; }
    
    [Required(ErrorMessage = "O campo Capacidade Total é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo Capacidade Total deve ser um número positivo.")]
    public int CapacidadeTotal { get; set; }
}
