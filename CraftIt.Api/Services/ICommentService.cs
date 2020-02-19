using System.Collections.Generic;
using CraftIt.Api.Models;
namespace CraftIt.Api.Services
{
    /// <summary>Class <c>ICommentService</c>Class used to set out the required interactions with the database for comments</summary>
    public interface ICommentService
    {
        Comment GetComment(int id);
        void AddComment(CommentCreationDto comment, User user);
        void DeleteComment(int id);
        void UpdateComment(CommentUpdateDto comment);
        bool Save();
    }
}