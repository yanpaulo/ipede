using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public int? MainImageId { get; set; }

        public string Name { get; set; }
        
        public string ShortDescription { get; set; }
        
        public string FullDescription { get; set; }
        
        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public string MainImageUrl { get; set; }

        public bool IsSuggested { get; set; }
    }
}