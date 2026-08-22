using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Repository.Context;

namespace Repository.Employees
{
    public class EmployeeRepository(WebapiDbContext repositoryContext) : BaseRepository<Employee>(repositoryContext), IEmployeeRepository
    {
    }
}