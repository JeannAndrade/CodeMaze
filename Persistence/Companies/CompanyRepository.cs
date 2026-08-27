using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Companies
{
    public class CompanyRepository(WebapiDbContext repositoryContext) : BaseRepository<Company>(repositoryContext), ICompanyRepository
    {
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

        public async Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges) =>
            await FindByCondition(c => c.Id == companyId, trackChanges).SingleOrDefaultAsync();
    }
}
