using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Repository.Context;

namespace Repository.Companies
{
    public class CompanyRepository(WebapiDbContext repositoryContext) : BaseRepository<Company>(repositoryContext), ICompanyRepository
    {
    }
}