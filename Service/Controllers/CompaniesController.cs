using Application.Companies.Commands.CreateCompany;
using Application.Companies.Exceptions;
using Application.Companies.Queries.GetCompany;
using Application.Companies.Queries.GetCompanyList;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController(
  IGetCompaniesListQuery getCompaniesListQuery,
  IGetCompanyQuery getCompanyQuery,
  ICreateCompanyCommand createCompanyCommand) : ControllerBase
{
  private readonly IGetCompaniesListQuery _getCompaniesListQuery = getCompaniesListQuery;
  private readonly IGetCompanyQuery _getCompanyQuery = getCompanyQuery;
  private readonly ICreateCompanyCommand _createCompanyCommand = createCompanyCommand;

  [HttpGet]
  public async Task<IActionResult> GetCompanies()
  {
    var companies = await _getCompaniesListQuery.ExecuteAsync();
    return Ok(companies);
  }

  [HttpGet("{id:guid}", Name = "CompanyById")]
  public async Task<IActionResult> GetCompany(Guid id)
  {
    var company = await _getCompanyQuery.ExecuteAsync(id);

    return Ok(company);
  }

  [HttpPost]
  public async Task<IActionResult> CreateCompany([FromBody] CompanyModelForCreation company)
  {
    if (company is null)
      throw new CompanyForCreationNullException("CompanyModelForCreation object is null");

    var createdCompany = await _createCompanyCommand.ExecuteAsync(company);

    return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
  }
}
