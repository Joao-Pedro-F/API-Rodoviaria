namespace API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;
using System.ComponentModel.DataAnnotations;

public class RequisicaoCriarViagemDTO
{
    [Required(ErrorMessage = "A data de saída é obrigatória.")]
    public DateTime DataSaida { get; set; }
    [Required(ErrorMessage = "A data de chegada é obrigatória.")]
    public DateTime DataChegada { get; set; }
    [Required(ErrorMessage = "O ID do ônibus é obrigatório.")]
    public int OnibusId { get; set; } = 0;
    [Required(ErrorMessage = "O ID do motorista é obrigatório.")]
    public int MotoristaId { get; set; } = 0;
    [Required(ErrorMessage = "O ID da rota é obrigatório.")]
    public int RotaId { get; set; } = 0;
}
