namespace API_Rodoviaria.Application.DTO_s.Resposta.ViagemRespostaDTO
{
    public class RespostaCriarViagemDTO
    {
        public int Id { get; set; }
        public string EnderecoOrigem { get; set; } = string.Empty;
        public string EnderecoFim { get; set; } = string.Empty;
        public DateTime DataSaida { get; set; }
        public DateTime DataChegada { get; set; }

        public string Placa { get; set; } = string.Empty;
        public int CapacidadeTotal { get; set; }
    }
}
