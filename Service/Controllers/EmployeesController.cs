using Application.Employees.Queries.GetEmployee;
using Application.Employees.Queries.GetEmployeesList;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController(
        IGetEmployeesListQuery getEmployeesListQuery,
        IGetEmployeeQuery getEmployeeQuery) : Controller
    {

        private readonly IGetEmployeesListQuery _getEmployeesListQuery = getEmployeesListQuery;
        private readonly IGetEmployeeQuery _getEmployeeQuery = getEmployeeQuery;

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _getEmployeesListQuery.Execute(companyId);
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = _getEmployeeQuery.Execute(companyId, id);
            return Ok(employee);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}