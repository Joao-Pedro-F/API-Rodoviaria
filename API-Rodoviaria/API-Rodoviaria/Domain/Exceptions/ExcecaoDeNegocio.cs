namespace API_Rodoviaria.Domain.Exceptions
{
    public class ExcecaoDeNegocio : Exception

    {
        public int StatusCode { get; }
        public ExcecaoDeNegocio(string mensagem,int statusCode=400): base(mensagem)
        {
            statusCode = statusCode;
        }
    }
}
