namespace TestApplication.Models.DataModels
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
