using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Abstractions;
using Veterinary.Domain.Data;
using Veterinary.Domain.Models;

namespace Veterinary.DataAccessLayer
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext _db;
        public OwnerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public int InsertOwner(Owner owner)
        {
            if (owner == null)
            {
                throw new NullReferenceException();
            }
            _db.Owners.Add(owner);
            _db.SaveChanges();
            return owner.OwnerId;
        }

        public void EditOwner(Owner owner)
        {
            _db.Owners.Update(owner);
            _db.SaveChanges();
        }

        public void RemoveOwner(int ownerId)
        {
            var owner = _db.Owners.FirstOrDefault(o=>o.OwnerId==ownerId);
            _db.Owners.Remove(owner);
            _db.SaveChanges();
        }

        public IEnumerable<Pet> GetPets(int ownerId)
        {
            List<Pet> pets = new List<Pet>();
            foreach (var pet in _db.Pets)
            {
                if (pet.OwnerId == ownerId)
                {
                    //pet.UpcomingProcedures.Vaccines = GetUpcomingVaccines(pet.PetId);
                    //pet.UpcomingProcedures.Operations = GetUpcomingOperations(pet.PetId);
                    pets.Add(pet);
                }
            }
            return pets;
        }

        public IEnumerable<Vaccine> GetUpcomingVaccines(int petId)
        {
            List<Vaccine> vaccines = new List<Vaccine>();
            foreach (var vaccine in _db.Vaccines)
            {
                if (vaccine.PetId == petId && vaccine.NextVaccinationDate.Value.Date > DateTime.Now.Date)
                    vaccines.Add(vaccine);
            }
            return vaccines;
        }

        public IEnumerable<Operation> GetUpcomingOperations(int petId)
        {
            List<Operation> operations = new List<Operation>();
            foreach (var operation in _db.Operations)
            {
                if (operation.PetId == petId && operation.OperationDate.Date > DateTime.Now.Date)
                    operations.Add(operation);
            }
            return operations;
        }

        //public async Task<int> InsertOwnerAsync(Owner owner)
        //{
        //    var myTask = Task.Run(() => InsertOwner(owner));
        //    return await myTask;
        //}
    }
}
