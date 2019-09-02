using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CraftIt.Api.Models 
{
    public class CommentCreationDto
    {
        public int ProductId {get;set;}
        public string Message {get;set;}
    }
}