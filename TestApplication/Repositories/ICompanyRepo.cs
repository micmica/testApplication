using TestApplication.Models.DataModels;

namespace TestApplication.Repositories
{
    public interface ICompanyRepo
    {
        //Task<Company> CreateCompany(Company model);
        //Task<IEnumerable<Company>> GetCompanyByName(string name);
        Task<bool> IsNameUniqueAsync(string name);
        Task AddAsync(Company company);
        Task<Company> GetByIdAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
