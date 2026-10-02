namespace API_Rodoviaria.Application.DTO_s.Resposta.MotoristaRespostaDTO
{
    public class RespostaCriarMotoristaDTO
    {
        public RespostaCriarMotoristaDTO(int id, string nome, string cpf, string? cnh)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Cnh = cnh;
        }

        public int Id { get; set; }
        public string Nome { get; set; }= string.Empty;
        public string Cpf { get; set; }= string.Empty;
        public string Cnh { get; set; } = string.Empty;
    }
}
