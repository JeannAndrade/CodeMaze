using Application.Companies;

namespace Service.Companies
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Country { get; set; }

        public static CompanyDto FromApplication(CompanyModel company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };
        }

        public static List<CompanyDto> FromApplication(List<CompanyModel> companies)
        {
            return companies.Select(FromApplication).ToList();
        }
    }
}