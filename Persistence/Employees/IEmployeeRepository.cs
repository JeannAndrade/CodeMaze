using Domain.Models;

namespace Persistence.Employees
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees(Guid companyId, bool trackChanges);
    }
}