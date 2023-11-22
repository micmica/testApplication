namespace TestApplication.Models.QueryModels
{
    public class CompanyQuery
    {
        public string? Name { get; set; }
        public List<EmployeeRequest>? Employees { get; set; }
    }

    public class EmployeeRequest
    {
        public string? Email { get; set; }
        public int? Id { get; set; }
    }
}
