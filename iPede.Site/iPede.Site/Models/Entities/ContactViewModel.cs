using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class ContactViewModel
    {
        [Required]
        [Display(Name="Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name="Mensagem")]
        public string Content { get; set; }


    }
}