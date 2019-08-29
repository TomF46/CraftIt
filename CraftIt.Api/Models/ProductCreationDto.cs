using System.Collections.Generic;

namespace CraftIt.Api.Models 
{
    public class ProductCreationDto{
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> Requirements {get;set; }
        public string TimeEstimate {get;set;}
        public ICollection<InstructionCreationDto> Instructions {get;set;}
        public string ProductImage{ get; set; }
    }
}