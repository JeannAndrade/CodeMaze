
using Domain.Models;

namespace Application.Companies
{
    public record CompanyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Country { get; set; }

        public static CompanyModel FromDomain(Company company)
        {
            return new CompanyModel
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };
        }
    }
}