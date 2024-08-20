using EmployeeAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Tests.Helpers
{
    public class DbContextFixture : IDisposable
    {
        public DbContextOptions<EmployeeContext> ContextOptions { get; private set; }
        public EmployeeContext Context { get; private set; }

        public DbContextFixture()
        {
            ContextOptions = new DbContextOptionsBuilder<EmployeeContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new EmployeeContext(ContextOptions);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
