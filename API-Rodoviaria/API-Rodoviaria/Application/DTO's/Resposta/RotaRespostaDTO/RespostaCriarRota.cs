namespace API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO
{
    public class RespostaCriarRota
    {
        public int Id { get; set; }
        public string EnderecoInicio { get; set; } = string.Empty;
        public string EnderecoFim { get; set; } = string.Empty;
    }
}
