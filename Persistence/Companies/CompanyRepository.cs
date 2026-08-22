using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Persistence.Context;

namespace Persistence.Companies
{
    public class CompanyRepository(WebapiDbContext repositoryContext) : BaseRepository<Company>(repositoryContext), ICompanyRepository
    {
        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();
    }
}