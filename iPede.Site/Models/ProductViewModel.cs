using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iPede.Site.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public ProductViewModel()
        {
            Product = new Product();
        }

        public ProductViewModel(Product product)
        {
            this.Product = product;
        }

        public int Id 
        {
            get
            {
                return Product.Id;
            }
            set
            {
                Product.Id = value;
            }
        }

        [Display(Name="Categoria")]
        public int CategoryId 
        {
            get
            {
                return Product.CategoryId;
            }
            set
            {
                Product.CategoryId = value;
            }
        }

        public int? MainImageId 
        { 
            get
            {
                return Product.MainImageId;
            }
            set
            {
                Product.MainImageId = value;
            }
        }

        [Required]
        [Display(Name = "Nome")]
        public string Name 
        { 
            get
            {
                return Product.Name;
            }
            set
            {
                Product.Name = value;
            }
        }

        [Required]
        [Display(Name = "Descrição curta", Description = "A descrição que aparece próximo ao nome do produtos")]
        public string ShortDescription
        {
            get 
            { 
                return Product.ShortDescription; 
            }
            set
            {
                Product.ShortDescription = value;
            }
        }


        [Display(Name = "Descrição detalhada")]
        public string FullDescription 
        { 
            get
            {
                return Product.FullDescription;
            }
            set
            {
                Product.FullDescription = value;
            }
        }

        [Display(Name="Preço")]
        public decimal Price
        {
            get
            {
                return Product.Price;
            }
            set
            {
                Product.Price = value;
            }
        }

        public List<ProductImage> Images { get; set; }

        public int? DefaultImageIndex { get; set; }

        public ProductImage MainImage { get; set; }
    }
}