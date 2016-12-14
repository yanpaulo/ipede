using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public int? ParentCategoryId { get; set; }
        
        public string Name { get; set; }

        public string ParentCategoryName { get; set; }

        public virtual ICollection<CategoryDTO> SubCategories { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}