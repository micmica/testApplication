namespace TestApplication.Models.DataModels
{
    public class SystemLog
    {
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public int ResourceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Event { get; set; }
        public string Changeset { get; set; }
        public string Comment { get; set; }
    }
}
