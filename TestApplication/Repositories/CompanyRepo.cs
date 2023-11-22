using Microsoft.EntityFrameworkCore;
using TestApplication.Models;
using TestApplication.Models.DataModels;

namespace TestApplication.Repositories
{
    public class CompanyRepo : ICompanyRepo
    {
        private readonly Context _context;
        //private readonly EmployeeValidationService _validationService;
        public CompanyRepo(Context context)
        {
            _context = context;
        }
        public async Task<bool> IsNameUniqueAsync(string name)
        {
            return await _context.Companies.AllAsync(c => c.Name != name);
        }

        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _context.Companies
                .Include(c => c.Employees)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //public async Task<Company> CreateCompany(Company model)
        //{
        //    model.CreatedAt = DateTime.Now;
        //    List<Employee> newEmployees = new List<Employee>();

        //    foreach (var employeeData in model.Employees)
        //    {
        //        Employee employee;

        //        if (employeeData.Id != null && employeeData.Id != 0)
        //        {
        //            employee = _context.Employees.Find(employeeData.Id);
        //        }
        //        else
        //        {
        //            // Create a new employee
        //            employee = new Employee
        //            {
        //                Title = employeeData.Title,
        //                Email = employeeData.Email,
        //                CreatedAt = DateTime.Now
        //            };

        //            // Add the new employee to the list
        //            if (!model.Employees.Any(e => e.Id == employee.Id))
        //            {
        //                newEmployees.Add(employee);

        //                var logE = new SystemLog
        //                {
        //                    ResourceType = "Employee",
        //                    ResourceId = model.Id,
        //                    CreatedAt = DateTime.Now,
        //                    Event = "create",
        //                    Changeset = $"Employee created with email {employee.Email}",
        //                    Comment = $"New employee {employee.Email} was created."
        //                };

        //                _context.SystemLogs.Add(logE);
        //            }
        //        }

        //        // Do any other processing with the employee here
        //    }

        //    // Add new employees to the model after the foreach loop
        //    model.Employees.AddRange(newEmployees);

        //    _context.Companies.Add(model);

        //    var logC = new SystemLog
        //    {
        //        ResourceType = "Company",
        //        ResourceId = model.Id,
        //        CreatedAt = DateTime.Now,
        //        Event = "create",
        //        Changeset = $"Company created with email {model.Name}",
        //        Comment = $"New company {model.Name} was created."
        //    };

        //    _context.SystemLogs.Add(logC);

        //    await _context.SaveChangesAsync();
        //    return model;
        //}
    }
}
