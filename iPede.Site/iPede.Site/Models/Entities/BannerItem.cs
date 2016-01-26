using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public enum BannerContentVerticalPosition
    {
        [Display(Name="Meio")]
        Middle,
        [Display(Name="Topo")]
        Top,
        [Display(Name ="Embaixo")]
        Bottom
    }
    public enum UrlType
    {
        [Display(Name="Interno")]
        Internal,
        [Display(Name="Externo")]
        External
    }

    public class BannerItem
    {
        
        public int BannerItemId { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name="Texto")]
        public string Text { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Display(Name="Posição")]
        public BannerContentVerticalPosition VerticalPosition { get; set; }

        [Display(Name="Link")]
        public string LinkUrl { get; set; }

        [Display(Name="Tipo de link")]
        public UrlType LinkUrlType { get; set; }

    }
}