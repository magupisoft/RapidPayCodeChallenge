using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Data.Repositories;
using RapidPay.Domain.Requests;

namespace RapidPay.CardManagement;

public class UserSecurityService(
IAccountRepository accountRepository,
IConfiguration configuration) : IUserSecurityService
{
    public async Task<(string?, DateTime?)> GetAccessToken(UserLoginRequest request)
    {
        var account = await accountRepository.GetAccountAsync(request.Username);
        if (account == null)
        {
            return (null, null);
        }

        if (account.Password == request.Password)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            _ = int.TryParse(configuration["Jwt:DurationInMinutes"], out int duration);
            var key = Encoding.ASCII.GetBytes
            (configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, account.Email),
                    new Claim(JwtRegisteredClaimNames.Email, account.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(duration),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return (jwtToken, tokenDescriptor.Expires);
        }

        return (null, null);
    }
}
