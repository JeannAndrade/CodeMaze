
using LumiaFoundation.AspNetCore.Commons.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Companies.Exceptions
{

    public class CompanyForCreationNullException(string message) : DomainBaseException(message)
    {
        protected override int StatusCodeValue => StatusCodes.Status400BadRequest;
    }

}