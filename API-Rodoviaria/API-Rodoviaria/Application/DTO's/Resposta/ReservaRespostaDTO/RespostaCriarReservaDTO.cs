namespace API_Rodoviaria.Application.DTO_s.Resposta.ReservaRespostaDTO
{
    public class RespostaCriarReservaDTO
    {
        public int Id { get; set; }
        
        public int FkViagem { get; set; }

        public DateTime DataReserva { get; set; }
        public List<int> NumerosCadeiras { get; set; } = new();
    }
}
