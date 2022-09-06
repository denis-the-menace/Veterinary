using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinary.Domain.Models
{
    public class Vaccine : BaseEntity
    {
        [Key]
        public int VaccineId { get; set; }
        public VaccineType Name { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public DateTime? NextVaccinationDate { get; set; }
        [ForeignKey("Pet")]
        public int PetId { get; set; }

        public Vaccine()
        {
            if(VaccinationDate == null)
                VaccinationDate = DateTime.Now;
            if (NextVaccinationDate == null)
                NextVaccinationDate = DateTime.Now.AddMonths(1);
        }
    }
}
