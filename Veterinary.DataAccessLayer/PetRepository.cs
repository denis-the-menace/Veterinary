using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Abstractions;
using Veterinary.Domain.Data;
using Veterinary.Domain.Models;
using Veterinary.Domain.Models.API;

namespace Veterinary.DataAccessLayer
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _db;
        public PetRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public int InsertPet(Pet pet)
        {
            if (pet == null)
            {
                throw new NullReferenceException();
            }
            _db.Pets.Add(pet);
            _db.SaveChanges();
            return pet.PetId;
        }

        public void EditPet(Pet pet)
        {
            _db.Pets.Update(pet);
            _db.SaveChanges();
        }

        public void RemovePet(int petId)
        {
            var pet = _db.Pets.FirstOrDefault(p=>p.PetId==petId);
            _db.Pets.Remove(pet);
            _db.SaveChanges();
        }

        public int AddPetVaccine(Vaccine vaccine)
        {
            if (vaccine == null)
            {
                throw new NullReferenceException();
            }
            _db.Vaccines.Add(vaccine);
            _db.SaveChanges();
            return vaccine.VaccineId;
        }

        public int AddPetOperation(Operation operation)
        {
            if (operation == null)
            {
                throw new NullReferenceException();
            }
            _db.Operations.Add(operation);
            _db.SaveChanges();
            return operation.OperationId;
        }

        public MedicalDetails GetMedicalDetails(int petId)
		{
            List<Operation> operations = new List<Operation>();
            foreach (var operation in _db.Operations)
            {
                if (operation.PetId == petId)
                    operations.Add(operation);
            }
            List<Vaccine> vaccines = new List<Vaccine>();
            foreach (var vaccine in _db.Vaccines)
            {
                if (vaccine.PetId == petId)
                    vaccines.Add(vaccine);
            }
            var details = new MedicalDetails();
            details.Operations = operations;
            details.Vaccines = vaccines;
            return details;
        }

        public Vaccine GetVaccine(int vaccineId)
        {
            var vaccine = _db.Vaccines.Find(vaccineId);
            //if (vaccine == null)
            //{
            //    return new NullReferenceException();
            //}
            return vaccine;
        }
        public Operation GetOperation(int operationId)
        {
            var operation = _db.Operations.Find(operationId);
            //if (vaccine == null)
            //{
            //    return new NullReferenceException();
            //}
            return operation;
        }

        public void EditVaccine(Vaccine vaccine)
        {
            _db.Vaccines.Update(vaccine);
            _db.SaveChanges();
        }

        public void RemoveVaccine(int vaccineId)
        {
            var vaccine = GetVaccine(vaccineId);
            _db.Vaccines.Remove(vaccine);
            _db.SaveChanges();
        }
        public void EditOperation(Operation operation)
        {
            _db.Operations.Update(operation);
            _db.SaveChanges();
        }

        public void RemoveOperation(int operationId)
        {
            var operation = GetOperation(operationId);
            _db.Operations.Remove(operation);
            _db.SaveChanges();
        }
    }
}
