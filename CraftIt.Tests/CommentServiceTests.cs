using NUnit.Framework;
using CraftIt.Api.Models;
using CraftIt.Api.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace Tests
{
    [TestFixture]
    public class CommentServiceTests
    {

        private readonly CraftItContext _context;
        private readonly ICommentService _commentService;
        private readonly IProductRepository _productRepository;
        private User _testUser;
        private Product _testProduct;
    
        public CommentServiceTests()
        {
            _context = new InMemoryDbContextFactory().GetDbContext();
            _commentService = new CommentService(_context);
            _productRepository = new ProductRepository(_context);


            _testUser = new User{
                Id = 10,
                FirstName = "Test",
                LastName = "User",
                Username= "Test1User",
                PasswordHash = null,
                PasswordSalt = null
            };

            _testProduct = new Product{
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
        }

        [SetUp]
        public void SetUp(){
            _context.Products.RemoveRange(_context.Products);
            _context.Comments.RemoveRange(_context.Comments);
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        [TearDown]
        public void dispose(){
            _context.Products.RemoveRange(_context.Products);
            _context.Comments.RemoveRange(_context.Comments);
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        [Test]
        public void CanAddComment(){
            _context.Users.Add(_testUser);
            _context.Products.Add(_testProduct);
            _context.SaveChanges();

            var comment = new CommentCreationDto{
                ProductId = _testProduct.Id,
                Message = "My test message"
            };

            Assert.DoesNotThrow(() => {
                _commentService.AddComment(comment, _testUser);
                _commentService.Save();
            });
        }

        [Test]
        public void CanGetCommentOnProduct(){
            _context.Users.Add(_testUser);
            _context.Products.Add(_testProduct);
            _context.SaveChanges();

            var comment = new CommentCreationDto{
                ProductId = _testProduct.Id,
                Message = "My test message"
            };
            _commentService.AddComment(comment, _testUser);
            _commentService.Save();

            var product = _productRepository.GetProduct(_testProduct.Id);

            var firstComment = product.Comments.First();
            Assert.AreEqual(firstComment.Message, comment.Message);
        }

        [Test]
        public void CanDeleteComment(){
            _context.Users.Add(_testUser);
            _context.Products.Add(_testProduct);

            var comment = new Comment{
                Id = 2,
                Message = "Test comment",
                CreatedAt = DateTime.Now,
                Product = _testProduct,
                User = _testUser
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            _commentService.DeleteComment(2);
            _commentService.Save();

            var product = _productRepository.GetProduct(_testProduct.Id);

            var theComment = product.Comments.FirstOrDefault(x => x.Id == 2);

            Assert.IsNull(theComment);
        
        }

        [Test]
        public void CanUpdateComment(){
            _context.Users.Add(_testUser);
            _context.Products.Add(_testProduct);

            var comment = new Comment{
                Id = 3,
                Message = "Test comment",
                CreatedAt = DateTime.Now,
                Product = _testProduct,
                User = _testUser
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();

            var updatedComment = new CommentUpdateDto{
                CommentId = comment.Id,
                Message = "An updated message"
            };

            _commentService.UpdateComment(updatedComment);
            _commentService.Save();

            var product = _productRepository.GetProduct(_testProduct.Id);

            var theComment = product.Comments.FirstOrDefault(x => x.Id == 3);
            Assert.AreEqual(theComment.Message, updatedComment.Message);
        }

    
    }
}