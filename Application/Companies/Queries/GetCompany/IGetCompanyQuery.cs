
namespace Application.Companies.Queries.GetCompany
{
    public interface IGetCompanyQuery
    {
        CompanyModel Execute(Guid companyId);
    }
}