using System.Collections.Generic;
using CraftIt.Api.Models;
namespace CraftIt.Api.Services
{
    public interface ICommentService
    {
        Comment GetComment(int id);
        void AddComment(CommentCreationDto comment, User user);
        void DeleteComment(int id);
        void UpdateComment(CommentUpdateDto comment);
        bool Save();
    }
}