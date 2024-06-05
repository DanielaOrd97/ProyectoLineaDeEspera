using API_Linea_Espera.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Linea_Espera.Helpers
{
    public class TokenGeneratorJwt
    {

        public string GetToken(string nombre, string rol, string id)
        {
            List<Claim> claims = new();

            if (nombre=="Administrador")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
            }
            else if(rol=="Operador")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Operador"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Cliente"));
            }
            claims.Add(new Claim(ClaimTypes.Name, nombre));
            claims.Add(new Claim(ClaimTypes.Role, rol));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id));


            JwtSecurityTokenHandler handler = new();

            var token = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = "lienaespera.net",
                Audience = "AppLineaEspera",
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                NotBefore = DateTime.Now.AddMinutes(-1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("LALLAVESECRETADELBANCOESESTA")),
            SecurityAlgorithms.HmacSha256)
            };

            return handler.CreateEncodedJwt(token);
        }
    }
}
