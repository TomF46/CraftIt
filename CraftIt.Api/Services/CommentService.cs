using System;
using System.Collections.Generic;
using System.Linq;
using CraftIt.Api.Helpers;
using CraftIt.Api.Models;
using Microsoft.AspNetCore.Http;

namespace CraftIt.Api.Services
{
    public class CommentService : ICommentService
    {
        private CraftItContext _context;
        private IHttpContextAccessor _contextAccessor;


        public CommentService(CraftItContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public void AddComment(CommentCreationDto comment)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == int.Parse(_contextAccessor.HttpContext.User.Identity.Name));

            if(user == null) throw new AppException("User not found");

            var product = _context.Products.FirstOrDefault(x => x.Id == comment.ProductId);

            if(product == null) throw new AppException("Product not found");

            var commentToAdd = new Comment{
                User = user,
                Product = product,
                Message = comment.Message,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(commentToAdd);
        }

        public void DeleteComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(x => x.Id == id);

            if(comment == null) throw new AppException("Comment not found");

            if(comment.User.Id != int.Parse(_contextAccessor.HttpContext.User.Identity.Name)) throw new AppException("User does not have permission to delete this comment");

            _context.Comments.Remove(comment);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateComment(CommentUpdateDto comment)
        {
            var commentToUpdate = _context.Comments.FirstOrDefault(x => x.Id == comment.CommentId);

            if(commentToUpdate == null) throw new AppException("Comment not found");

            if(commentToUpdate.User.Id != int.Parse(_contextAccessor.HttpContext.User.Identity.Name)) throw new AppException("User does not have permission to update this comment");

            commentToUpdate.Message = comment.Message;
            Save();
        }
    }
}