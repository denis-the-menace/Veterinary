using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Models;

namespace Veterinary.Domain.Abstractions
{
    public interface IOwnerRepository
    {
        IEnumerable<Pet> GetPets(int ownerId);
        int InsertOwner(Owner owner);
        public void EditOwner(Owner owner);
        public void RemoveOwner(int ownerId);
        IEnumerable<Vaccine> GetUpcomingVaccines(int petId);
        public IEnumerable<Operation> GetUpcomingOperations(int petId);
    }
}
