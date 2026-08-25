using Application.Companies.Queries.GetCompany;
using Application.Companies.Queries.GetCompanyList;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController(
  IGetCompaniesListQuery getCompaniesListQuery,
  IGetCompanyQuery getCompanyQuery) : ControllerBase
{
  private readonly IGetCompaniesListQuery _getCompaniesListQuery = getCompaniesListQuery;
  private readonly IGetCompanyQuery _getCompanyQuery = getCompanyQuery;

  [HttpGet]
  public IActionResult GetCompanies()
  {
    var companies = _getCompaniesListQuery.Execute();
    return Ok(companies);
  }

  [HttpGet("{id:guid}")]
  public IActionResult GetCompany(Guid id)
  {
    var company = _getCompanyQuery.Execute(id);

    return Ok(company);
  }
}
