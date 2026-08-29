
using Application.Employees.Commands.CreateEmployee;

namespace Service.Employees
{
    public record EmployeeForCreationDto(string Name, int Age, string Position);

    public static class EmployeeForCreationDtoExtensions
    {
        public static EmployeeModelForCreation ToCreateCompanyCommand(this EmployeeForCreationDto dto) => new(dto.Name, dto.Age, dto.Position);
    }
}