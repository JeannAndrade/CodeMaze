using LumiaFoundation.EFRepository.Repository;
using Persistence.Companies;
using Persistence.Employees;

namespace Persistence.Management
{
    public interface IRepositoryManager : IBaseRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
    }
}