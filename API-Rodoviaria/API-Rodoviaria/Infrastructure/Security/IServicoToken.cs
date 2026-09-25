using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Rodoviaria.Domain.Models;
using Microsoft.IdentityModel.Tokens;
namespace API_Rodoviaria.Infrastructure.Security;
public record TokenGerado(string Token, DateTime ExpiraEm);
public interface IServicoToken
{
    TokenGerado GerarToken(Usuario usuario);


}
