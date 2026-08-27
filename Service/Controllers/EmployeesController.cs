using Application.Employees.Queries.GetEmployeesList;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController(IGetEmployeesListQuery getEmployeesListQuery) : Controller
    {

        private readonly IGetEmployeesListQuery _getEmployeesListQuery = getEmployeesListQuery;

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _getEmployeesListQuery.Execute(companyId);
            return Ok(employees);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}