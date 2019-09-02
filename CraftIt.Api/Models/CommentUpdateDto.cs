using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CraftIt.Api.Models 
{
    public class CommentUpdateDto
    {
        public int CommentId {get;set;}
        public string Message {get;set;}
    }
}