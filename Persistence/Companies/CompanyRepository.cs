using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Persistence.Context;

namespace Persistence.Companies
{
    public class CompanyRepository(WebapiDbContext repositoryContext) : BaseRepository<Company>(repositoryContext), ICompanyRepository
    {
        public IEnumerable<Company> GetAllCompanies(bool trackChanges) => [.. FindAll(trackChanges).OrderBy(c => c.Name)];
        public Company? GetCompany(Guid companyId, bool trackChanges) => FindByCondition(c => c.Id == companyId, trackChanges).SingleOrDefault();
    }
}