namespace Veterinary.Models
{
    public class MedicalDetails
    {
        public IEnumerable<Operation> Operations { get; set; }
        public IEnumerable<Vaccine> Vaccines { get; set; }
    }
}
