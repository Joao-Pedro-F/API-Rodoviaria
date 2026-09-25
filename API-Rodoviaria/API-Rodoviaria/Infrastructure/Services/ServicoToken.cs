using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Rodoviaria.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using API_Rodoviaria.Infrastructure.Security;
namespace API_Rodoviaria.Infrastructure.Services
{
    public class ServicoToken
    {
        private readonly JwtConfiguracoes _config;

        public TokenGerado GerarToken(Usuario usuario)
        {
            var claims= new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new(ClaimTypes.Name, usuario.Username),
                new(ClaimTypes.Role, usuario.Perfil.Cargo),
            };
            
            var chave= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Chave));

            var credenciais= new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var expiraEm = DateTime.UtcNow.AddMinutes(_config.ExpiracaoMinutos);

            var jwt= new JwtSecurityToken(
                issuer: _config.Emissor,
                audience: _config.Audiencia,
                claims: claims,
                expires: expiraEm,
                signingCredentials: credenciais);

            return new TokenGerado(new JwtSecurityTokenHandler().WriteToken(jwt),expiraEm);
        }
    }
    

    
}
