using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Veterinary.Domain.Abstractions;
using Veterinary.Domain.Models;
using Veterinary.Domain.Models.API;

namespace VeterinaryAPI.Controllers
{
    [Route("api/vet/")]
    [ApiController]
    public class VetController : ControllerBase
    {
        private readonly IVetService _service;

        public VetController(IVetService service)
        {
            _service = service;
        }

        [HttpPost("clients"), Authorize]
        public void AddClient(OwnerRegister ownerRegister)
        {
            int vetId = int.Parse(User.Claims.Where(v => v.Type == "VetId").Select(u => u.Value).SingleOrDefault());
            Owner owner = new Owner
            {
                Name = ownerRegister.Name,
                Surname = ownerRegister.Surname,
                Email = ownerRegister.Email,
                VetId = vetId
            };
            if(owner!=null)
                _service.InsertOwner(owner, vetId);
        }
        [HttpPost("pets"), Authorize]
        public void AddPet(PetRegister petRegister)
        {
            int vetId = int.Parse(User.Claims.Where(v => v.Type == "VetId").Select(u => u.Value).SingleOrDefault());
            Pet pet = new Pet
            {
                Name = petRegister.Name,
                Birthdate = petRegister.Birthdate,
                Color = petRegister.Color,
                Gender = petRegister.Gender,
                Neutered = petRegister.Neutered,
                OwnerId = petRegister.OwnerId
            };
            if (pet != null)
                _service.InsertPet(pet, vetId);
        }

        [HttpGet("clients"),Authorize]
        public IEnumerable<Owner> GetClients()
        {
            int vetId = int.Parse(User.Claims.Where(v => v.Type == "VetId").Select(u => u.Value).SingleOrDefault());
            var clients = _service.GetClients(vetId);
            return clients;
        }
        [HttpGet("pets/{id}"), Authorize]
        public IActionResult GetPets(int id)
        {
            var pets = _service.GetPets(id);
            return Ok(pets);
        }

        [HttpPost("pets/{id}/vaccine"),Authorize]
        public void AddPetVaccine(Vaccine vaccine)
        {
            _service.AddPetVaccine(vaccine);
        }

        [HttpPost("pets/{id}/operation"), Authorize]
        public void AddPetOperation(Operation operation)
        {
            _service.AddPetOperation(operation);
        }
        [HttpGet("pets/{id}/details"),Authorize]
        public MedicalDetails GetMedicalDetails(int id)
		{
            return _service.GetMedicalDetails(id);
		}

        [HttpGet("vaccine/{vaccineId}"),Authorize]
        public Vaccine GetVaccine(int vaccineId)
        {
            return _service.GetVaccine(vaccineId);
        }

        [HttpGet("operation/{operationId}"),Authorize]
        public Operation GetOperation(int operationId)
        {
            return _service.GetOperation(operationId);
        }

        [HttpPut("vaccine/{vaccineId}"),Authorize]
        public void EditVaccine(Vaccine vaccine)
        {
            _service.EditVaccine(vaccine);
        }

        [HttpDelete("vaccine/{vaccineId}"),Authorize]
        public void RemoveVaccine(int vaccineId)
        {
            _service.RemoveVaccine(vaccineId);
        }

        [HttpPut("operation/{operationId}"),Authorize]
        public void EditOperation(Operation operation)
        {
            _service.EditOperation(operation);
        }

        [HttpDelete("operation/{operationId}"),Authorize]
        public void RemoveOperation(int operationId)
        {
            _service.RemoveOperation(operationId);
        }
    }
}
