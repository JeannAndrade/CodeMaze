
using Domain.Models;

namespace Application.Employees
{
    public record EmployeeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Position { get; set; }

        public static EmployeeModel FromDomain(Employee employee)
        {
            return new EmployeeModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Position = employee.Position
            };
        }
    }
}
