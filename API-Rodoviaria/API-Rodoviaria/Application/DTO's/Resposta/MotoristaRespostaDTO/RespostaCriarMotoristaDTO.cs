namespace API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO
{
    public class RespostaCriarMotoristaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }= string.Empty;
        public string Cpf { get; set; }= string.Empty;
        public string Cnh { get; set; } = string.Empty;
    }
}
