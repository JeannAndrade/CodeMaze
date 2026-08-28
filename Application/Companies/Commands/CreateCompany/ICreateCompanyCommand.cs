
namespace Application.Companies.Commands.CreateCompany
{
    public interface ICreateCompanyCommand
    {
        Task<CompanyModel> ExecuteAsync(CompanyModelForCreation companyModel);
    }
}