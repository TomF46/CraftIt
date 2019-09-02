using NUnit.Framework;
using CraftIt.Api.Models;
using CraftIt.Api.Services;


namespace Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {

        private readonly CraftItContext _context;
        private readonly IProductRepository _productRepository;
    
        public ProductRepositoryTests()
        {
            _context = new InMemoryDbContextFactory().GetDbContext();
            _productRepository = new ProductRepository(_context);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestOne()
        {
            Assert.IsTrue(true);
        }

    }
}