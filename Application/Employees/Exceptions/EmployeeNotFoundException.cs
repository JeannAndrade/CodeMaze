using LumiaFoundation.AspNetCore.Commons.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Employees.Exceptions
{
    public class EmployeeNotFoundException(string message) : DomainBaseException(message)
    {
        protected override int StatusCodeValue => StatusCodes.Status404NotFound;
    }
}