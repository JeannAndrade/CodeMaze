using LumiaFoundation.EFRepository.Repository;
using Repository.Companies;
using Repository.Context;
using Repository.Employees;

namespace Repository.Management
{
    public class RepositoryManager(WebapiDbContext repositoryContext) : BaseRepositoryManager(repositoryContext), IRepositoryManager
    {
        private readonly Lazy<ICompanyRepository> _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
        private readonly Lazy<IEmployeeRepository> _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));

        public ICompanyRepository Company => _companyRepository.Value;

        public IEmployeeRepository Employee => _employeeRepository.Value;
    }
}