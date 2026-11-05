namespace API_Rodoviaria.Application.DTO_s.Resposta.CadeiraRespostaDTO
{
    public class RespostaCriarCadeiraDTO
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public bool Disponivel { get; set; }
        public int FkOnibus { get; set; }
    }
}
