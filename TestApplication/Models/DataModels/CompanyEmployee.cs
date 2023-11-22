namespace TestApplication.Models.DataModels
{
    public class CompanyEmployee
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
