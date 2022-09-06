using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Models;
using Veterinary.Domain.Models.API;

namespace Veterinary.Domain.Abstractions
{
    public interface IVetRepository
    {
        Vet FindVetByEmail(string vetEmail);
        IEnumerable<Owner> GetClients(int vetId);
        //int InsertVet(Vet vet);
        Task<int> InsertVetAsync(Vet vet);
        //void AddClient(Owner owner, int vetId);
    }
}
