using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CraftIt.Api.Models 
{
    public class Comment
    {
        public int Id { get; set; }
        public Product Product {get;set;}
        public User User {get;set;}
        public string Message {get;set;}
        public DateTime CreatedAt {get;set;}
    }
}