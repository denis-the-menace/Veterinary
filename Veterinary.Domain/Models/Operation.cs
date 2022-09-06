using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinary.Domain.Models
{
    public class Operation : BaseEntity
    {
        [Key]
        public int OperationId { get; set; }
        public OperationType Name { get; set; }
        public DateTime OperationDate { get; set; }
        [ForeignKey("Pet")]
        public int PetId { get; set; }

        public Operation()
        {
            if(OperationDate==null)
                OperationDate = DateTime.Now;
        }
    }
}
