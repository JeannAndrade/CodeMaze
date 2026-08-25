
using LumiaFoundation.AspNetCore.Commons.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Companies.Exceptions
{
    public class CompanyNotFoundException(string message) : DomainBaseException(message)
    {
        protected override int StatusCodeValue => StatusCodes.Status404NotFound;
    }
}