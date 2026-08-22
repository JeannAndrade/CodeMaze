using Application.Companies.Queries.GetCompanyList;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController(
  IGetCompaniesListQuery getCompaniesListQuery) : ControllerBase
{
  private readonly IGetCompaniesListQuery _getCompaniesListQuery = getCompaniesListQuery;

  [HttpGet]
  public IActionResult GetCompanies()
  {
    try
    {
      var companies = _getCompaniesListQuery.Execute();
      return Ok(companies);
    }
    catch
    {
      return StatusCode(500, "Internal server error");
    }
  }
}
