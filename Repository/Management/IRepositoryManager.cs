using LumiaFoundation.EFRepository.Repository;
using Repository.Companies;
using Repository.Employees;

namespace Repository.Management
{
    public interface IRepositoryManager : IBaseRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
    }
}