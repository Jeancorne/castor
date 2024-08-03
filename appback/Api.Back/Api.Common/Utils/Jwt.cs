using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Common.Utils
{
    public class Jwt
    {
        private readonly byte[] secret;

        public Jwt(string secretKey)
        {
            this.secret = Encoding.ASCII.GetBytes(secretKey);
        }

        public string createToken(List<string> claimsList, Guid id, string name, string lastName)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("idUser", id.ToString()));
            claims.AddClaim(new Claim("name", name));
            claims.AddClaim(new Claim("lastName", lastName));
            foreach (var item in claimsList)
            {
                if (item != null)
                {
                    claims.AddClaim(new Claim("Token", item));
                }
            }
            SecurityTokenDescriptor tokenDescripcion = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.secret),
                SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler token = new JwtSecurityTokenHandler();
            var createdToken = token.CreateToken(tokenDescripcion);
            return token.WriteToken(createdToken);
        }
    }
}