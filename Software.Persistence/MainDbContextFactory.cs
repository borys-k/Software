using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Software.Persistence
{
    class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=FileStorageMain;Trusted_Connection=True");

            return new MainDbContext(optionsBuilder.Options);
        }
    }
}
