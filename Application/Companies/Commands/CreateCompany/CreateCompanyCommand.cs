
using LumiaFoundation.Core.Validators;
using Persistence.Management;

namespace Application.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand(IRepositoryManager repositoryManager) : ICreateCompanyCommand
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;

        public async Task<CompanyModel> ExecuteAsync(CompanyModelForCreation companyModel)
        {
            // Execute the validator.
            CommandValidator.Validate(companyModel);

            var company = companyModel.ToDomain();
            _repositoryManager.Company.CreateCompany(company);
            await _repositoryManager.SaveAsync();

            return CompanyModel.FromDomain(company);
        }
    }
}