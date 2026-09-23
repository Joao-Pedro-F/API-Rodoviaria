namespace API_Rodoviaria.Application.DTO_s.Requisicao.ReservaRequisicaoDTO;
using System.ComponentModel.DataAnnotations;

public class RequisicaoCriarReservaDTO
{
    [Required(ErrorMessage = "O campo Id do Cliente é obrigatório.")]
    public int FkUsuario { get; set; }
    [Required(ErrorMessage = "O campo Id da Viagem é obrigatório.")]
    public int FkViagem { get; set; }
    //[Required(ErrorMessage = "O campo Data da Reserva é obrigatório.")]
    //public DateTime Criacao { get; set; } = DateTime.Now;


}
