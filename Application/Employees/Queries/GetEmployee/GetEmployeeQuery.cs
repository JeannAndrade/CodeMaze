using LumiaFoundation.Core.Domain.Exceptions;
using Persistence.Management;

namespace Application.Employees.Queries.GetEmployee
{
    public class GetEmployeeQuery(IRepositoryManager repositoryManager) : IGetEmployeeQuery
    {
        private readonly IRepositoryManager _repository = repositoryManager;

        public async Task<EmployeeModel> ExecuteAsync(Guid companyId, Guid id)
        {
            var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges: false) ?? throw new EntityNotFoundException("Employee not found");

            return new EmployeeModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Position = employee.Position
            };
        }
    }
}
