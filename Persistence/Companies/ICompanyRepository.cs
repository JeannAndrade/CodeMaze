using Domain.Models;

namespace Persistence.Companies
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges);
    }
}
