using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Persistence.Context;

namespace Persistence.Companies
{
    public class CompanyRepository(WebapiDbContext repositoryContext) : BaseRepository<Company>(repositoryContext), ICompanyRepository
    {
    }
}