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
    public class VetRepository : IVetRepository
    {
        private readonly ApplicationDbContext _db;
        public VetRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        //public async Task<Vet> FindVetByName(string vetName)
        //{
        //    var vet = await _db.Vets.FirstOrDefault(v => v.Name == vetName);

        //}

        public Vet FindVetByEmail(string vetEmail)
        {
            Vet vet = _db.Vets.Where(v => v.Email == vetEmail).FirstOrDefault();
            return vet;
        }


        public IEnumerable<Owner> GetClients(int vetId)
        {
            List<Owner> clients = new List<Owner>();
            foreach(var owner in _db.Owners)
            {
                if (owner.VetId == vetId)
                    clients.Add(owner);
            }
            return clients;
        }
        //public async Task<IEnumerable<Owner>> GetClientsAsync(int vetId)
        //{
        //    var myTask = Task.Run(() => GetClients(vetId));
        //    return await myTask;
        //}

        private int InsertVet(Vet vet)
        {
            if (vet == null)
            {

            }
            _db.Vets.Add(vet);
            _db.SaveChanges();
            return vet.VetId;
        }

        public async Task<int> InsertVetAsync(Vet vet)
        {
            var myTask = Task.Run(() => InsertVet(vet));
            return await myTask;
        }

        //public int InsertOwner(Owner owner)
        //{
        //    if (owner == null)
        //    {
        //        throw new NullReferenceException();
        //    }
        //    _db.Owners.Add(owner);
        //    _db.SaveChanges();
        //    return owner.OwnerId;
        //}

        //public async Task<int> InsertOwnerAsync(Owner owner)
        //{
        //    var myTask = Task.Run(() => InsertOwner(owner));
        //    return await myTask;
        //}

        //public void AddClient(Owner owner, int vetId)
        //{
        //    _db.Vets.Find(vetId).Clients.Add(owner);
        //    var clients = _db.Vets.Find(vetId).Clients;
        //}
    }
}
