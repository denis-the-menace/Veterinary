namespace Veterinary.Models
{
    public class Vaccine
    {
        public int VaccineId { get; set; }
        public VaccineType Name { get; set; }
        public DateTime VaccinationDate { get; set; }
        public DateTime? NextVaccinationDate { get; set; }
        public int PetId { get; set; }
    }
}
