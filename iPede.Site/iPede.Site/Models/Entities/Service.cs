using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Ícone")]
        public string Icon { get; set; }
    }
}