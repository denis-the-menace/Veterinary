using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Domain.Models.API;

namespace Veterinary.Domain.Models
{
    public class Pet : BaseEntity
    {
        [Key]
        public int PetId { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public byte[]? Image { get; set; }
        public PetType Type { get; set; }
        public string Color { get; set; }
        public char Gender { get; set; }
        public bool Neutered { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
        //public virtual MedicalDetails UpcomingProcedures { get; set; }
        public Pet()
        {
            if (Birthdate == null)
                Birthdate = DateTime.Now;
        }
    }
}
