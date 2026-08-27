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
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId)
        {
            var employees = await _getEmployeesListQuery.ExecuteAsync(companyId);
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = await _getEmployeeQuery.ExecuteAsync(companyId, id);
            return Ok(employee);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
