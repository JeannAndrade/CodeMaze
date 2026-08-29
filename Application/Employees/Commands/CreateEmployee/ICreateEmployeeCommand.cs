
namespace Application.Employees.Commands.CreateEmployee
{
    public interface ICreateEmployeeCommand
    {
        Task<EmployeeModel> ExecuteAsync(Guid companyId, EmployeeModelForCreation employeeModel);
    }
}