using NUnit.Framework;
using CraftIt.Api.Models;
using CraftIt.Api.Services;
using System.Collections.ObjectModel;
using System.Linq;

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
        public void CanAddProduct()
        {
            var user = new User{
                Id = 10,
                FirstName = "Test",
                LastName = "User",
                Username= "Test1User",
                PasswordHash = null,
                PasswordSalt = null
            };

            _context.Users.Add(user);

            Assert.DoesNotThrow(() => {
                var instructions = new Collection<InstructionCreationDto>();
                var requirements = new Collection<string>();

                var product = new ProductCreationDto{
                    Name = "Test product",
                    Description = "A test product",
                    TimeEstimate = "5 hours",
                    Requirements = requirements,
                    Instructions = instructions,
                    ProductImage = null
                };

                _productRepository.AddProduct(product, user);
            });

        }
    }
}