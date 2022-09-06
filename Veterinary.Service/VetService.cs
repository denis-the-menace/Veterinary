using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Abstractions;
using Veterinary.Domain.Models;
using Veterinary.Domain.Models.API;

namespace Veterinary.Service
{
    public class VetService : IVetService
    {
        private readonly IVetRepository _vetRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPetRepository _petRepository;

        public VetService(IVetRepository repository, IOwnerRepository ownerRepository, IPetRepository petRepository)
        {
            _vetRepository = repository;
            _ownerRepository = ownerRepository;
            _petRepository = petRepository;
        }

        public Vet FindVetByEmail(string vetEmail)
        {
            return _vetRepository.FindVetByEmail(vetEmail);
        }

        public async Task<int> InsertVetAsync(Vet vet)
        {
            return await _vetRepository.InsertVetAsync(vet);
        }

        public int InsertOwner(Owner owner, int vetId)
        {
            var id = _ownerRepository.InsertOwner(owner);
            return id;
        }

        public int InsertPet(Pet pet, int ownerId)
        {
            var id = _petRepository.InsertPet(pet);
            return id;
        }

        public IEnumerable<Owner> GetClients(int vetId)
        {
            var clients = _vetRepository.GetClients(vetId);
            return clients;
        }
        public IEnumerable<Pet> GetPets(int ownerId)
        {
            var pets = _ownerRepository.GetPets(ownerId);
            return pets;
        }

        public int AddPetVaccine(Vaccine vaccine)
        {
            return _petRepository.AddPetVaccine(vaccine);
        }
        public int AddPetOperation(Operation operation)
        {
            return _petRepository.AddPetOperation(operation);
        }
        public MedicalDetails GetMedicalDetails(int petId)
		{
            return _petRepository.GetMedicalDetails(petId);
		}
        public Vaccine GetVaccine(int vaccineId)
        {
            return _petRepository.GetVaccine(vaccineId);
        }
        public Operation GetOperation(int operationId)
        {
            return _petRepository.GetOperation(operationId);
        }
        public void EditVaccine(Vaccine vaccine)
        {
            _petRepository.EditVaccine(vaccine);
        }
        public void RemoveVaccine(int vaccineId)
        {
            _petRepository.RemoveVaccine(vaccineId);
        }
        public void EditOperation(Operation operation)
        {
            _petRepository.EditOperation(operation);
        }
        public void RemoveOperation(int operationId)
        {
            _petRepository.RemoveOperation(operationId);
        }
    }
}
