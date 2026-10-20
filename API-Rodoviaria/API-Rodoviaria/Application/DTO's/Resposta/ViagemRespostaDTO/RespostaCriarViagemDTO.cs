namespace API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO
{
    public class RespostaCriarViagemDTO
    {
        public int Id { get; set; }
        public string EnderecoOrigem { get; set; } = string.Empty;
        public string EnderecoFim { get; set; } = string.Empty;
        public DateTime DataSaida { get; set; }
        public DateTime DataChegada { get; set; }
        public string NomeMotorista { get; set; } = string.Empty;
        public int FkMotorista { get; set; }
        public int FkOnibus { get; set; }
        public int FkRota { get; set; }
        public string Placa { get; set; } = string.Empty;
        public int CapacidadeTotal { get; set; }
    }
}
