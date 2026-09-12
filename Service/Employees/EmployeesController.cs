using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Queries.GetEmployee;
using Application.Employees.Queries.GetEmployeesList;
using LumiaFoundation.AspNetCore.Commons.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Employees
{
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController(
        IGetEmployeesListQuery getEmployeesListQuery,
        IGetEmployeeQuery getEmployeeQuery,
        ICreateEmployeeCommand createEmployeeCommand
        ) : BaseApiController
    {

        private readonly IGetEmployeesListQuery _getEmployeesListQuery = getEmployeesListQuery;
        private readonly IGetEmployeeQuery _getEmployeeQuery = getEmployeeQuery;
        private readonly ICreateEmployeeCommand _createEmployeeCommand = createEmployeeCommand;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId)
        {
            var employees = await _getEmployeesListQuery.ExecuteAsync(companyId);
            return Ok(employees);
        }

        [HttpGet("{id:guid}", Name = "EmployeeById")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = await _getEmployeeQuery.ExecuteAsync(companyId, id);
            return Ok(employee);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employeeDto)
        {
            var createdEmployee = await _createEmployeeCommand.ExecuteAsync(companyId, employeeDto.ToCreateCompanyCommand());

            return CreatedAtRoute("EmployeeById", new { companyId, id = createdEmployee.Id }, createdEmployee);
        }
    }
}
