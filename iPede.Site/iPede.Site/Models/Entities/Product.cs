using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        
        [Required]
        [Display(Name="Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Descrição curta", Description="A descrição que aparece próximo ao nome do produtos")]
        public string ShortDescription { get; set; }

        [Display(Name="Descrição detalhada")]
        public string FullDescription { get; set; }

        [Display(Name="Preço")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }

        public int? MainImageId { get; set; }

        public virtual ProductImage MainImage { get; set; }

        /// <summary>
        /// Returns the product's main image. If there is no main image, then
        /// the static ProductImage.NoImage is returned.
        /// </summary>
        [NotMapped]
        public ProductImage MainOrNoImage
        {
            get
            {
                return MainImage ?? ProductImage.NoImage;
            }
        }
    }
}