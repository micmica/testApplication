namespace TestApplication.Models.DataModels
{
    public class Employee
    {
        public int Id { get; set; }
        public TitleEnum Title { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Company> Companies { get; set; } = new List<Company>();
    }
}
