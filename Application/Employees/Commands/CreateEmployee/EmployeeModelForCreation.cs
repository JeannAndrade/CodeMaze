
using Domain.Models;

namespace Application.Employees.Commands.CreateEmployee
{
    public record EmployeeModelForCreation(string Name, int Age, string Position)
    {
        public Employee ToDomain() => new()
        {
            Name = Name,
            Age = Age,
            Position = Position
        };
    }
}