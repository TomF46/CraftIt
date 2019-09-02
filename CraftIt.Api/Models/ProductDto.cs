using System.Collections.Generic;

namespace CraftIt.Api.Models 
{
    public class ProductDto{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TimeEstimate {get;set;}
        public string ProductImage{ get; set; }
    }
}