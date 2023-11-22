using TestApplication.Models.DataModels;
using TestApplication.Models.QueryModels;

namespace TestApplication.Services
{
    public interface ICompanyService
    {
        Task<Company> CreateCompanyAsync(CompanyQuery model);
    }
}