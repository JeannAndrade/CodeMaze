using Application.Employees.Exceptions;
using LumiaFoundation.Logger.Contracts;
using Persistence.Management;

namespace Application.Employees.Queries.GetEmployee
{
    public class GetEmployeeQuery(IRepositoryManager repositoryManager, ILoggerManager logger) : IGetEmployeeQuery
    {
        private readonly IRepositoryManager _repository = repositoryManager;
        private readonly ILoggerManager _logger = logger;

        public async Task<EmployeeModel> ExecuteAsync(Guid companyId, Guid id)
        {
            try
            {
                var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges: false) ?? throw new EmployeeNotFoundException("Employee not found");

                return new EmployeeModel
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Position = employee.Position
                };
            }
            catch (EmployeeNotFoundException)
            {
                _logger.LogWarn($"Employee with id: {id} doesn't exist in the database.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetEmployeeQuery)} service method {ex}");
                throw;
            }
        }
    }
}
