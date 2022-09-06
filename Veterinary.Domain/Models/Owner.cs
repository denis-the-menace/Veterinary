using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinary.Domain.Models
{
    public class Owner : BaseEntity
    {
        [Key]
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [ForeignKey("Vet")]
        public int VetId { get; set; }
        public virtual Vet? Vet { get; set; }
        public virtual List<Pet>? Pets { get; set; }
    }
}
