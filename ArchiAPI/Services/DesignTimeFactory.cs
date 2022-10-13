using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
namespace ArchiAPI.Services
{
    public sealed class DesignTimeFactory : IDesignTimeDbContextFactory<Repository>
    {
        public Repository CreateDbContext(string[] args)
        {
            return new Repository(new DbContextOptionsBuilder<Repository>().UseSqlServer(System.IO.File.ReadAllText("C:\\jwt.txt")).Options);
        }
    }
}
