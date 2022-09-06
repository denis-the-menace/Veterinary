namespace Veterinary.Models
{
    public class Operation
    {
        public int OperationId { get; set; }
        public OperationType Name { get; set; }
        public DateTime OperationDate { get; set; }
        public int PetId { get; set; }
    }
}
