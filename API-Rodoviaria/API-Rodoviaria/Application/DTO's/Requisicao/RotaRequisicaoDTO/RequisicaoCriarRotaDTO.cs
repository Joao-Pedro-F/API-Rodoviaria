namespace API_Rodoviaria.Application.DTO_s.Requisicao.RotaRequisicaoDTO;
using System.ComponentModel.DataAnnotations;

public class RequisicaoCriarRotaDTO
{
    [Required(ErrorMessage = "O endereço de início é obrigatório.")]
    public string EnderecoInicio { get; set; }

    [Required(ErrorMessage = "O endereço de destino é obrigatório.")]
    public string EnderecoDestino { get; set; }
}
