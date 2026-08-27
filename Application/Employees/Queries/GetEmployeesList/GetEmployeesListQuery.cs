
using LumiaFoundation.Logger.Contracts;
using Persistence.Management;

namespace Application.Employees.Queries.GetEmployeesList
{

    public class GetEmployeesListQuery(IRepositoryManager repositoryManager, ILoggerManager logger) : IGetEmployeesListQuery
    {
        private readonly IRepositoryManager _repository = repositoryManager;
        private readonly ILoggerManager _logger = logger;

        public List<EmployeeModel> Execute(Guid companyId)
        {
            try
            {
                var employees = _repository.Employee.GetAllEmployees(companyId, trackChanges: false);

                return [.. employees.Select(c => new EmployeeModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Age = c.Age,
                    Position = c.Position
                })];
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetEmployeesListQuery)} service method {ex}");
                throw;
            }
        }
    }
}