
namespace Application.Companies.Queries.GetCompanyList
{
    public interface IGetCompaniesListQuery
    {
        List<CompanyModel> Execute();
    }
}