using TestApplication.Models;
using TestApplication.Models.DataModels;

namespace TestApplication.Repositories
{
    public class SystemLogRepo : ISystemLogRepo
    {
        private readonly Context _context;
        public SystemLogRepo(Context context)
        {
            _context = context;
        }

        public async Task CreateLog(SystemLog log)
        {
            _context.SystemLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
