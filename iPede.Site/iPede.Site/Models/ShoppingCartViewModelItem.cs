using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iPede.Site.Models
{
    public class ShoppingCartViewModelItem
    {
        public Product Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Inclua ao menos um item.")]
        public int Ammount { get; set; }

    }
}