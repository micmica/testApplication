using TestApplication.Models.DataModels;
using TestApplication.Models.QueryModels;
using TestApplication.Repositories;

namespace TestApplication.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepo _companyRepository;
        private readonly IEmployeeRepo _employeeRepository;
        private readonly ISystemLogRepo _systemLogRepo;

        public CompanyService(ICompanyRepo companyRepository, IEmployeeRepo employeeRepository, ISystemLogRepo systemLogRepo)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _systemLogRepo = systemLogRepo;
        }

        public async Task<Company> CreateCompanyAsync(CompanyQuery model)
        {
            if (await _companyRepository.IsNameUniqueAsync(model.Name))
            {
                var company = new Company
                {
                    Name = model.Name,
                    CreatedAt = DateTime.UtcNow
                };

                foreach (var employeeReference in model.Employees)
                {
                    Employee employee;

                    if (employeeReference.Id.HasValue)
                    {
                        employee = await _employeeRepository.GetByIdAsync(employeeReference.Id.Value);
                    }
                    else if (!string.IsNullOrEmpty(employeeReference.Email))
                    {
                        employee = await _employeeRepository.GetByEmailAsync(employeeReference.Email);

                        // If the employee doesn't exist, create a new one
                        if (employee == null)
                        {
                            employee = new Employee
                            {
                                Email = employeeReference.Email,
                                CreatedAt = DateTime.UtcNow
                            };

                            await _employeeRepository.AddAsync(employee);
                            var logEmployee = new SystemLog
                            {
                                ResourceType = "Employee",
                                ResourceId = employee.Id,
                                CreatedAt = DateTime.Now,
                                Event = "create",
                                Changeset = $"Employee created with email {employee.Email}",
                                Comment = $"New employee {employee.Email} was created."
                            };

                            await _systemLogRepo.CreateLog(logEmployee);
                        }
                    }
                    else
                    {
                        throw new ValidationException("employees", "Each employee reference must have either an ID or an Email.");
                    }

                    if (employee != null)
                    {
                        company.Employees.Add(employee);
                    }
                }

                var log = new SystemLog
                {
                    ResourceType = "Company",
                    ResourceId = company.Id,
                    CreatedAt = DateTime.Now,
                    Event = "create",
                    Changeset = $"Company created with email {company.Name}",
                    Comment = $"New company {company.Name} was created."
                };

                await _systemLogRepo.CreateLog(log);

                await _companyRepository.AddAsync(company);
                await _companyRepository.SaveChangesAsync();

                return company;
            }
            else
            {
                throw new ValidationException("name", "Company name must be unique");
            }
        }
    }
}
