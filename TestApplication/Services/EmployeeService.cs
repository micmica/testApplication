using TestApplication.Models;
using TestApplication.Models.DataModels;
using TestApplication.Repositories;

namespace TestApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepository;
        private readonly ICompanyRepo _companyRepository;
        private readonly ISystemLogRepo _systemLogRepo;

        public EmployeeService(
            IEmployeeRepo employeeRepository,
            ICompanyRepo companyRepository,
            ISystemLogRepo systemLogRepo)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _systemLogRepo = systemLogRepo;
        }

        public async Task<Employee> CreateEmployeeAsync(string email, TitleEnum title, List<int> companyIds)
        {
           
            if (await _employeeRepository.IsEmailUniqueAsync(email))
            {
                foreach (var companyId in companyIds)
                {
                    if (await _employeeRepository.IsTitleUniqueInCompanyAsync(title, companyId))
                    {
                        var employee = new Employee
                        {
                            Email = email,
                            Title = title,
                            CreatedAt = DateTime.UtcNow
                        };

                        foreach (var id in companyIds)
                        {
                            var company = await _companyRepository.GetByIdAsync(id);
                            if (company != null)
                            {
                                company.Employees.Add(employee);
                            }
                        }

                        await _employeeRepository.AddAsync(employee);

                        var log = new SystemLog
                        {
                            ResourceType = "Employee",
                            ResourceId = employee.Id,
                            CreatedAt = DateTime.Now,
                            Event = "create",
                            Changeset = $"Employee created with email {employee.Email}",
                            Comment = $"New employee {employee.Email} was created."
                        };

                        await _systemLogRepo.CreateLog(log);

                        await _employeeRepository.SaveChangesAsync();

                        return employee;
                    }
                    else
                    {
                        throw new ValidationException("title", "Employee title must be unique within a company");
                    }
                }
                return null;
            }
            else
            {
                throw new ValidationException("email", "Employee email must be unique");
            }
        }
    }
}
