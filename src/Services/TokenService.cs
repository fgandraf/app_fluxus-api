using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluxusApi.Models;
using FluxusApi.Models.DTO;
using Microsoft.IdentityModel.Tokens;

namespace FluxusApi.Services;

public class TokenService
{
    public string GenerateToken(UserDTO userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userDto.UserName)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}