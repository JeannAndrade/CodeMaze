using LumiaFoundation.Logger.Contracts;
using Persistence.Management;

namespace Application.Companies.Queries.GetCompanyList
{
    public class GetCompaniesListQuery(IRepositoryManager repositoryManager, ILoggerManager logger) : IGetCompaniesListQuery
    {
        private readonly IRepositoryManager _repository = repositoryManager;
        private readonly ILoggerManager _logger = logger;

        public async Task<List<CompanyModel>> ExecuteAsync()
        {
            try
            {
                var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges: false);

                return [.. companies.Select(c => new CompanyModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Country = c.Country
                })];
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompaniesListQuery)} service method {ex}");
                throw;
            }
        }
    }
}
