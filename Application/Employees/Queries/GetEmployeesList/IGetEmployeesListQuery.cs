namespace Application.Employees.Queries.GetEmployeesList
{
    public interface IGetEmployeesListQuery
    {
        List<EmployeeModel> Execute(Guid companyId);
    }
}