using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinary.Domain.Models.API
{
    public class PetRegister
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public byte[]? Image { get; set; }
        public PetType Type { get; set; }
        public string Color { get; set; }
        public char Gender { get; set; }
        public bool Neutered { get; set; }
        public int OwnerId { get; set; }
    }
}
