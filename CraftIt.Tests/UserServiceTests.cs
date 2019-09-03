using NUnit.Framework;
using CraftIt.Api.Models;
using CraftIt.Api.Services;

namespace Tests
{
    [TestFixture]
    public class UserServiceTests
    {

        private readonly CraftItContext _context;
        private readonly IUserService _userService;
    
        public UserServiceTests()
        {
            _context = new InMemoryDbContextFactory().GetDbContext();
            _userService = new UserService(_context);
        }

        [SetUp]
        public void SetUp(){
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        [TearDown]
        public void dispose(){
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        [Test]
        public void UserCanRegister()
        {
            var user = new User{
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Username = "TestUser1"
            };

            _userService.Create(user, "Password1");

            var userFromDb = _userService.GetById(1);

            Assert.AreEqual(user.Username, userFromDb.Username);
        }

        [Test]
        public void UserCanRegisterThenLoginSuccesfully()
        {
            var user = new User{
                Id = 1,
                FirstName = "Test",
                LastName = "User2",
                Username = "TestUser2"
            };

            _userService.Create(user, "Password1");

            var authenticatedUser = _userService.Authenticate("TestUser2", "Password1");

            Assert.IsNotNull(authenticatedUser);
            
        }

        [Test]
        public void UserCanNotAuthenticateWithIncorrectPassword()
        {
            var user = new User{
                Id = 1,
                FirstName = "Test",
                LastName = "User3",
                Username = "TestUser3"
            };

            _userService.Create(user, "Password3");

            var authenticatedUser = _userService.Authenticate("TestUser3", "Password1");

            Assert.IsNull(authenticatedUser);
            
        }

        [Test]
        public void CanGetUser(){
            var user = new User{
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Username= "Test1User",
                PasswordHash = null,
                PasswordSalt = null
            };

            _context.Users.Add(user);

            var fromDb = _userService.GetById(1);

            Assert.AreEqual(fromDb.Username, "Test1User");
        }

        [Test]
        public void CanDeleteUser(){
            var user = new User{
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Username= "Test110User",
                PasswordHash = null,
                PasswordSalt = null
            };

            _context.Users.Add(user);

            var exists1 = _userService.GetById(1);

            Assert.IsNotNull(exists1);

            _userService.Delete(1);

            var exists2 = _userService.GetById(1);

            Assert.IsNull(exists2);
        }
    }
}