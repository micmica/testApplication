using TestApplication.Models;
using TestApplication.Models.DataModels;

namespace TestApplication.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> CreateEmployeeAsync(string email, TitleEnum title, List<int> companyIds);
    }
}
