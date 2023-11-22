using TestApplication.Models;
using TestApplication.Models.DataModels;

namespace TestApplication.Repositories
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> GetEmployeeByEmail(string email);
        //Task<Employee> CreateEmployee(Employee model);
        bool IsTitleAlreadyTaken(string title, int companyId);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsTitleUniqueInCompanyAsync(TitleEnum title, int companyId);
        Task AddAsync(Employee employee);
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> GetByEmailAsync(string email);
        Task<bool> SaveChangesAsync();
    }
}
