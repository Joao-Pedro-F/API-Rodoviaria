namespace API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO
{
    public class RespostaCriarOnibusDTO
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public int CapacidadeTotal { get; set; }
        public int FkViagem { get; set; }
        public int FkRota { get; set; }
        public int FkMotorista { get; set; }
        public string EnderecoInicio { get; set; } = string.Empty;
        public string EnderecoFim { get; set; } = string.Empty;
        public DateTime Datasaida { get; set; }
        public DateTime DataChegada { get; set; }

    }
}
