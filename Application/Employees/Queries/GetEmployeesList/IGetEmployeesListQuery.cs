namespace Application.Employees.Queries.GetEmployeesList
{
    public interface IGetEmployeesListQuery
    {
        Task<List<EmployeeModel>> ExecuteAsync(Guid companyId);
    }
}
