using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CraftIt.Api.Models 
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Username {get;set;}
        public string Message {get;set;}
        public DateTime CreatedAt {get;set;}
    }
}