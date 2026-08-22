using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Persistence.Context;

namespace Persistence.Employees
{
    public class EmployeeRepository(WebapiDbContext repositoryContext) : BaseRepository<Employee>(repositoryContext), IEmployeeRepository
    {
    }
}