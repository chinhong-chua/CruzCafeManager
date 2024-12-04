namespace CafeBackend.Application.Features.Cafe.Queries.GetAllCafes
{
    public class CafeDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Employees { get; set; }
        public string? Logo { get; set; }
        public required string Location { get; set; }
    }
}
