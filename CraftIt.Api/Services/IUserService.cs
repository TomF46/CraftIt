using System.Collections.Generic;
using CraftIt.Api.Models;

namespace CraftIt.Api.Services
{
    /// <summary>Class <c>IUserService</c>Class used to set out the required interactions with the database for user management</summary>
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}