﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AirTickets.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace AirTickets.Identity
{
    public class TokenClass
    {
        private readonly string _tokenSecret = "ThisIsASecretKeyForJwtAuthorization";
        private static readonly TimeSpan _tokenLifeTime = TimeSpan.FromHours(24);

        public string GenerateToken([FromBody] TokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_tokenSecret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, request.Email!),
                new(JwtRegisteredClaimNames.Email, request.Email!),
                new("userId", request.Id.ToString()),
                new("userRole", request.Role!.ToString()),
                new("userEmail", request.Email!.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_tokenLifeTime),
                Issuer = "http://localhost:8762",
                Audience = "http://localhost:8762",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
