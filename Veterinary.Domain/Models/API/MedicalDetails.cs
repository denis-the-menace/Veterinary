using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinary.Domain.Models.API
{
	public class MedicalDetails
	{
		public int Id { get; set; }
		public IEnumerable<Operation> Operations { get; set; }
		public IEnumerable<Vaccine> Vaccines { get; set; }
	}
}
