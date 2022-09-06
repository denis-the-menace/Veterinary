namespace Veterinary.Models
{
	public class Pet
	{
        public int PetId { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public byte[]? Image { get; set; }
        public PetType Type { get; set; }
        public string Color { get; set; }
        public char Gender { get; set; }
        public bool Neutered { get; set; }
        public int OwnerId { get; set; }
        public MedicalDetails UpcomingProcedures { get; set; }
    }
}
