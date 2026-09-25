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
   

    [Range(1, int.MaxValue, ErrorMessage = "Informe o motorista do Ônibus")]
    public int FkMotorista {  get; set; }

    [Required(ErrorMessage = "O Endereço de início da Rota é obrigatório.")]
    [StringLength(200)]
    public string EnderecoInicio { get; set; }= string.Empty;

    [Required(ErrorMessage = "O Endereço de fim da Rota é obrigatório.")]
    [StringLength(200)]
    public string EnderecoFim { get; set; }= string.Empty ;

    public DateTimeOffset DataSaida { get; set; }
    public DateTimeOffset DataChegada { get; set; }



}

