using Domain.Models;

namespace Persistence.Employees
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(Guid companyId, bool trackChanges);
        Task<Employee?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
    }
}
