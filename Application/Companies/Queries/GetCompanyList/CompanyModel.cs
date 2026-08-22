
namespace Application.Companies.Queries.GetCompanyList
{
    public record CompanyModel
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Country { get; set; }
    }
}