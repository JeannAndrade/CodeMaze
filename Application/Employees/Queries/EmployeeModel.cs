
namespace Application.Employees.Queries
{
    public record EmployeeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Position { get; set; }
    }
}