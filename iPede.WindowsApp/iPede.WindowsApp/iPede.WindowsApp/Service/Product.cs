using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace iPede.WindowsApp.Service
{
    public class Product
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

        public string MainImageThumbUrl { get; set; }

        public bool IsSuggested { get; set; }
        
    }
}
