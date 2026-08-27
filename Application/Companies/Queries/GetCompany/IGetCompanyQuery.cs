
namespace Application.Companies.Queries.GetCompany
{
    public interface IGetCompanyQuery
    {
        Task<CompanyModel> ExecuteAsync(Guid companyId);
    }
}
