using Application.Companies;
using Application.Companies.Commands.CreateCompany;
using Application.Companies.Queries.GetCompany;
using Application.Companies.Queries.GetCompanyList;
using LumiaFoundation.AspNetCore.Commons.BaseControllers;
using LumiaFoundation.AspNetCore.ExceptionHandlers.ErrorModel;
using Microsoft.AspNetCore.Mvc;

namespace Service.Companies;

[ApiController]
[Route("api/companies")]
public class CompaniesController(
  IGetCompaniesListQuery getCompaniesListQuery,
  IGetCompanyQuery getCompanyQuery,
  ICreateCompanyCommand createCompanyCommand) : BaseApiController
{
  private readonly IGetCompaniesListQuery _getCompaniesListQuery = getCompaniesListQuery;
  private readonly IGetCompanyQuery _getCompanyQuery = getCompanyQuery;
  private readonly ICreateCompanyCommand _createCompanyCommand = createCompanyCommand;

  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<CompanyModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<IEnumerable<CompanyModel>>> GetCompanies()
  {
    var companies = await _getCompaniesListQuery.ExecuteAsync();
    return Ok(companies);
  }

  [HttpGet("{id:guid}", Name = "CompanyById")]
  [ProducesResponseType(typeof(CompanyModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<CompanyModel>> GetCompany(Guid id)
  {
    var company = await _getCompanyQuery.ExecuteAsync(id);

    return Ok(company);
  }

  [HttpPost]
  [ProducesResponseType(typeof(CompanyModel), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status422UnprocessableEntity)]
  public async Task<ActionResult<CompanyModel>> CreateCompany([FromBody] CompanyForCreationDto company)
  {
    var createdCompany = await _createCompanyCommand.ExecuteAsync(company.ToCreateCompanyCommand());

    return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
  }
}
