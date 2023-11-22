using TestApplication.Models.DataModels;

namespace TestApplication.Repositories
{
    public interface ISystemLogRepo
    {
        Task CreateLog(SystemLog log);
    }
}
