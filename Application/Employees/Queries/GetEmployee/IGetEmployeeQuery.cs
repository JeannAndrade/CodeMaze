namespace Application.Employees.Queries.GetEmployee
{
    public interface IGetEmployeeQuery
    {
        EmployeeModel Execute(Guid companyId, Guid id);
    }
}