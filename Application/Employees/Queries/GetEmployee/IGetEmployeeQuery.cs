namespace Application.Employees.Queries.GetEmployee
{
    public interface IGetEmployeeQuery
    {
        Task<EmployeeModel> ExecuteAsync(Guid companyId, Guid id);
    }
}
