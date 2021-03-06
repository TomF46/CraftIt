using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CraftIt.Api.Models 
{
    public class Product
    {
        public int Id { get; set; }
        public User AddedBy {get;set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements {get;set; }
        public string TimeEstimate {get;set;}
        public ICollection<Instruction> Instructions {get;set;}
        public ICollection<Comment> Comments {get;set;}

        public byte[] ProductImage{ get; set; }
    }
}