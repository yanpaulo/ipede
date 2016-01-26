using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name="Subcategoria de ")]
        public int? ParentCategoryId { get; set; }

        [Required]
        [Display(Name="Nome")]
        public string Name { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        [NotMapped]
        public string FullName 
        {
            get
            {
                string name = Name;
                if (ParentCategoryId.HasValue)
                {
                    name = ParentCategory.FullName + "/" + name;
                }
                return name;
            }
        }

        
    }
}