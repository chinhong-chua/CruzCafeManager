namespace CafeBackend.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public required string Id { get; set; } // Unique identifier in 'UIXXXXXXX' format
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public Guid? CafeId { get; set; }
        public Cafe? Cafe { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
