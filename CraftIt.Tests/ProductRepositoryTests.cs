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

        private User _testUser;
    
        public ProductRepositoryTests()
        {
            _context = new InMemoryDbContextFactory().GetDbContext();
            _productRepository = new ProductRepository(_context);

            _testUser = new User{
                Id = 10,
                FirstName = "Test",
                LastName = "User",
                Username= "Test1User",
                PasswordHash = null,
                PasswordSalt = null
            };
        }

        [SetUp]
        public void SetUp(){
            _context.Products.RemoveRange(_context.Products);
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        [TearDown]
        public void dispose(){
            _context.Products.RemoveRange(_context.Products);
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        [Test]
        public void CanAddProduct()
        {

            _context.Users.Add(_testUser);

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

                _productRepository.AddProduct(product, _testUser);
            });

        }

        [Test]
        public void CanGetProduct(){

            _context.Users.Add(_testUser);

            var product = new Product{
                Id = 1,
                Name = "Test product 2",
                Description = "A description",
                AddedBy = _testUser,
                TimeEstimate = "5 hours",
                ProductImage = null,
                Requirements = "[]",
                Comments = new Collection<Comment>(),
                Instructions = new Collection<Instruction>()
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            var fromDb = _productRepository.GetProduct(1);

            Assert.AreEqual(fromDb.Id, product.Id);
        }

        [Test]
        public void CanGetMultipleProducts()
        {
            _context.Users.Add(_testUser);

            var product = new Product{
                Id = 1,
                Name = "Test product 2",
                Description = "A description",
                AddedBy = _testUser,
                TimeEstimate = "5 hours",
                ProductImage = null,
                Requirements = "[]",
                Comments = new Collection<Comment>(),
                Instructions = new Collection<Instruction>()
            };

            _context.Products.Add(product);

            var product2 = new Product{
                Id = 2,
                Name = "Test product 3",
                Description = "A description",
                AddedBy = _testUser,
                TimeEstimate = "5 hours",
                ProductImage = null,
                Requirements = "[]",
                Comments = new Collection<Comment>(),
                Instructions = new Collection<Instruction>()
            };

            _context.Products.Add(product2);
            _context.SaveChanges();

            var products = _productRepository.GetAllProducts();

            Assert.AreEqual(products.Count(), 2);
        }

        [Test]
        public void CanDeleteProduct()
        {
            _context.Users.Add(_testUser);

            var product = new Product{
                Id = 1,
                Name = "Test product 2",
                Description = "A description",
                AddedBy = _testUser,
                TimeEstimate = "5 hours",
                ProductImage = null,
                Requirements = "[]",
                Comments = new Collection<Comment>(),
                Instructions = new Collection<Instruction>()
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            _productRepository.DeleteProduct(product);
            _productRepository.Save();

            var fromDB = _productRepository.GetProduct(product.Id);

            Assert.IsNull(fromDB);
        }

        [Test]
        public void CanUpdateProduct(){
            _context.Users.Add(_testUser);

            var product = new Product{
                Id = 1,
                Name = "Test product 2",
                Description = "A description",
                AddedBy = _testUser,
                TimeEstimate = "5 hours",
                ProductImage = null,
                Requirements = "[]",
                Comments = new Collection<Comment>(),
                Instructions = new Collection<Instruction>()
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            var updatedProduct = new ProductUpdateDto{
                Id = 1,
                Name = "Edited test product",
                Description = "A description",
                TimeEstimate = "5 hours",
                ProductImage = null,
                Requirements = new Collection<string>(),
                Instructions = new Collection<InstructionCreationDto>(),
            };

            _productRepository.UpdateProduct(updatedProduct);

            var fromDb = _productRepository.GetProduct(1);

            Assert.AreEqual(fromDb.Name, "Edited test product");
        }
    }
}