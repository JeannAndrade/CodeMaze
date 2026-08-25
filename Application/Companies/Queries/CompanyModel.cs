
namespace Application.Companies.Queries
{
    public record CompanyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Country { get; set; }
    }
}