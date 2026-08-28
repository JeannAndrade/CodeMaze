using Domain.Models;

namespace Application.Companies.Commands.CreateCompany
{
    public record CompanyModelForCreation(string Name, string Address, string Country)
    {
        public Company ToDomain() => new()
        {
            Name = Name,
            Address = Address,
            Country = Country
        };
    }
}
