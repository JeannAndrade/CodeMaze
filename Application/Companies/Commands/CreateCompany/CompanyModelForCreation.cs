using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.Companies.Commands.CreateCompany
{
    public record CompanyModelForCreation(
        [property: Required(ErrorMessage = "Company name is required")]
        [property: MaxLength(60, ErrorMessage = "Company name should not exceed 60 characters")]
        string Name,

        [property: Required(ErrorMessage = "Company address is required")]
        [property: MaxLength(60, ErrorMessage = "Company address should not exceed 60 characters")]
        string Address,

        [property: MaxLength(60, ErrorMessage = "Company country should not exceed 60 characters")]
        string Country)
    {
        public Company ToDomain() => new()
        {
            Name = Name,
            Address = Address,
            Country = Country
        };
    }
}
