using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Abstractions;
using Veterinary.Domain.Models;
using Veterinary.Domain.Models.API;

namespace Veterinary.Service
{
    public class AuthService : IAuthService
    {
        private readonly IVetService _service;
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;

        public AuthService(IVetService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        public Vet FindVetByEmail(string vetEmail)
        {
            return _service.FindVetByEmail(vetEmail);
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        public async Task<Vet> Create(VetRegister request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var vet = new Vet {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            
            await _service.InsertVetAsync(vet);
            return vet;
        }

        public bool VerifyPasswordHash(string password, Vet vet)
        {
            using (var hmac = new HMACSHA512(vet.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(vet.PasswordHash);
            }
        }
        public string CreateToken(Vet vet)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("VetId", vet.VetId.ToString()),
                new Claim("Name", vet.Name),
                new Claim("Surname", vet.Surname),
                new Claim("Email", vet.Email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: "localhost:44326",
                audience: "localhost:44326",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
