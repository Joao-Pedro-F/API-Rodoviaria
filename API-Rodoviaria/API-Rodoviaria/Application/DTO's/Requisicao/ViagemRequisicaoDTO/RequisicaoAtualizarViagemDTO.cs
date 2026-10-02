using System.ComponentModel.DataAnnotations;
namespace API_Rodoviaria.Application.DTO_s.Requisicao.ViagemRequisicaoDTO;

public class RequisicaoAtualizarViagemDTO
{
    [Range(1, int.MaxValue)] public int Id { get; set; }
    public DateTime DataSaida { get; set; }
    public DateTime DataChegada { get; set; }

}
