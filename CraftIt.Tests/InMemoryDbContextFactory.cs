using CraftIt.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class InMemoryDbContextFactory
    {
        public CraftItContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<CraftItContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                            .Options;
            var dbContext = new CraftItContext(options);
    
            return dbContext;
        }
    }
}