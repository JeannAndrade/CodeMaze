
namespace Application.Companies.Queries.GetCompanyList
{
    public interface IGetCompaniesListQuery
    {
        Task<List<CompanyModel>> ExecuteAsync();
    }
}
