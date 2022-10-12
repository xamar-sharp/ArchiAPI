using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
namespace ArchiAPI.Services
{
    public sealed class DesignTimeFactory : IDesignTimeDbContextFactory<Repository>
    {
        public Repository CreateDbContext(string[] args)
        {
            return new Repository(new DbContextOptionsBuilder<Repository>().UseSqlServer(args[0]).Options);
        }
    }
}
