using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Models;
using Veterinary.Domain.Models.API;

namespace Veterinary.Domain.Abstractions
{
    public interface IAuthService
    {
        Vet FindVetByEmail(string vetEmail);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        Task<Vet> Create(VetRegister request);
        bool VerifyPasswordHash(string password, Vet vet);
        string CreateToken(Vet vet);
    }
}
