using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Persistence.Context;

namespace Persistence.Employees
{
    public class EmployeeRepository(WebapiDbContext repositoryContext) : BaseRepository<Employee>(repositoryContext), IEmployeeRepository
    {
        public IEnumerable<Employee> GetAllEmployees(Guid companyId, bool trackChanges) =>
        [.. FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name)];

        public Employee? GetEmployee(Guid companyId, Guid id, bool trackChanges) => FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges).SingleOrDefault();
    }
}