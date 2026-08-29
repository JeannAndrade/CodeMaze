
using Application.Companies.Exceptions;
using Persistence.Management;

namespace Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand(IRepositoryManager repositoryManager) : ICreateEmployeeCommand
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        public async Task<EmployeeModel> ExecuteAsync(Guid companyId, EmployeeModelForCreation employeeModel)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(companyId, false) ?? throw new CompanyNotFoundException("Company not found");

            var employee = employeeModel.ToDomain();
            _repositoryManager.Employee.CreateEmployeeForCompany(companyId, employee);
            await _repositoryManager.SaveAsync();

            return EmployeeModel.FromDomain(employee);
        }
    }
}