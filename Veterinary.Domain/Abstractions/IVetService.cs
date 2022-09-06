using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Models;
using Veterinary.Domain.Models.API;

namespace Veterinary.Domain.Abstractions
{
    public interface IVetService
    {
        Vet FindVetByEmail(string vetEmail);
        Task<int> InsertVetAsync(Vet vet);
        int InsertOwner(Owner owner, int vetId);
        IEnumerable<Owner> GetClients(int vetId);
        int InsertPet(Pet pet, int ownerId);
        IEnumerable<Pet> GetPets(int ownerId);
        int AddPetVaccine(Vaccine vaccine);
        int AddPetOperation(Operation operation);
        MedicalDetails GetMedicalDetails(int petId);
        Vaccine GetVaccine(int vaccineId);
        Operation GetOperation(int operationId);
        void EditVaccine(Vaccine vaccine);
        void RemoveVaccine(int vaccineId);
        void EditOperation(Operation operation);
        void RemoveOperation(int operationId);
    }
}
