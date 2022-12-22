using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace CompanyEmployees.WebApi.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(connectionString: configuration.GetConnectionString("sqlConnection"),
                sqlServerOptionsAction: b => b.MigrationsAssembly("CompanyEmployees.WebApi"));

            return new RepositoryContext(options: builder.Options);
        }
    }
}
