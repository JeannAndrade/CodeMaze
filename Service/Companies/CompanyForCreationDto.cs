
using Application.Companies.Commands.CreateCompany;

namespace Service.Companies
{
    public record CompanyForCreationDto(string Name, string Address, string Country);

    public static class CompanyForCreationDtoExtensions
    {
        public static CompanyModelForCreation ToCreateCompanyCommand(this CompanyForCreationDto dto) => new(dto.Name, dto.Address, dto.Country);
    }
}