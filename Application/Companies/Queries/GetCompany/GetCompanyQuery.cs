using Application.Companies.Exceptions;
using LumiaFoundation.Logger.Contracts;
using Persistence.Management;

namespace Application.Companies.Queries.GetCompany
{
    public class GetCompanyQuery(IRepositoryManager repositoryManager, ILoggerManager logger) : IGetCompanyQuery
    {
        private readonly IRepositoryManager _repository = repositoryManager;
        private readonly ILoggerManager _logger = logger;

        public async Task<CompanyModel> ExecuteAsync(Guid companyId)
        {
            try
            {
                var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false) ?? throw new CompanyNotFoundException("Company not found");

                return new CompanyModel
                {
                    Id = company.Id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };
            }
            catch (CompanyNotFoundException)
            {
                _logger.LogWarn($"Company with id: {companyId} doesn't exist in the database.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanyQuery)} service method {ex}");
                throw;
            }
        }
    }
}
