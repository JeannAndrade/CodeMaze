using LumiaFoundation.Core.Domain.Exceptions;
using LumiaFoundation.Logger.Contracts;
using Persistence.Management;

namespace Application.Companies.Queries.GetCompany
{
    public class GetCompanyQuery(IRepositoryManager repositoryManager) : IGetCompanyQuery
    {
        private readonly IRepositoryManager _repository = repositoryManager;

        public async Task<CompanyModel> ExecuteAsync(Guid companyId)
        {

            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false) ?? throw new EntityNotFoundException("Company not found");

            return new CompanyModel
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };
        }
    }
}
