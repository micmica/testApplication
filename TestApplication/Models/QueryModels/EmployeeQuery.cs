namespace TestApplication.Models.QueryModels
{
    public class EmployeeQuery
    {
        public string Title { get; set; }
        public string Email { get; set; }

        public List<int> CompanyIds { get; set; }
    }
}
