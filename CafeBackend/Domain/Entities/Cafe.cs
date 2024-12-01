namespace CafeBackend.Domain.Entities
{
    public class Cafe : BaseEntity
    {
        public Guid Id { get; set; } // UUID
        public required string Description { get; set; }
        public byte[]? Logo { get; set; }
        public required string Location { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
