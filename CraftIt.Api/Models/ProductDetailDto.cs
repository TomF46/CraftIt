using System.Collections.Generic;

namespace CraftIt.Api.Models 
{
    public class ProductDetailDto{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> Requirements {get;set; }
        public string TimeEstimate {get;set;}
        public ICollection<InstructionDto> Instructions {get;set;}
        public string ProductImage{ get; set; }
        public ICollection<CommentDto> Comments {get;set;}
    }
}