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
  public async Task<IActionResult> GetCompanies()
  {
    var companies = await _getCompaniesListQuery.ExecuteAsync();
    return Ok(companies);
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetCompany(Guid id)
  {
    var company = await _getCompanyQuery.ExecuteAsync(id);

    return Ok(company);
  }
}
