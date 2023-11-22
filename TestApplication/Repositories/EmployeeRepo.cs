using Microsoft.EntityFrameworkCore;
using TestApplication.Models;
using TestApplication.Models.DataModels;

namespace TestApplication.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly Context _context;
        public EmployeeRepo(Context context)
        {
            _context = context;
        }

        public bool IsTitleAlreadyTaken(string title, int companyId)
        {
            if (!Enum.TryParse(typeof(TitleEnum), title, out object titleEnum))
            {
                return false;
            }

            return _context.Employees.Any(e => e.Title == (TitleEnum)titleEnum && e.Companies.Any(c => c.Id == companyId));
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByEmail(string email) 
        {
            return await _context.Employees.Where(e => e.Email == email).ToListAsync();
        }
        //public async Task<Employee> CreateEmployee(Employee model)
        //{

        //    model.CreatedAt=DateTime.Now;

        //    _context.Employees.Add(model);

        //    // Create SystemLog
        //    var log = new SystemLog
        //    {
        //        ResourceType = "Employee",
        //        ResourceId = model.Id,
        //        CreatedAt = DateTime.Now,
        //        Event = "create",
        //        Changeset = $"Employee created with email {model.Email}",
        //        Comment = $"New employee {model.Email} was created."
        //    };

        //    _context.SystemLogs.Add(log);

        //    await _context.SaveChangesAsync();
        //    return model;
        //}

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _context.Employees.AllAsync(e => e.Email != email);
        }

        public async Task<bool> IsTitleUniqueInCompanyAsync(TitleEnum title, int companyId)
        {
            var result= await _context.Employees.Include(c=>c.Companies)
                .AnyAsync(e => e.Title == title && e.Companies.Any(c => c.Id == companyId));
            return !result;
        }

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Companies)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<Employee> GetByEmailAsync(string email)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
