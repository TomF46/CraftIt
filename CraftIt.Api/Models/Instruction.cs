using Microsoft.EntityFrameworkCore;

namespace CraftIt.Api.Models 
{
    public class Instruction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Ordinal {get;set;}
        public byte[] Image {get;set;}
    }
}