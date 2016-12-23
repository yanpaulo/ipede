using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class OrderStatus
    {
        public static readonly string
            Open = "Aberto",
            Placed = "Realizado",
            Confirmed = "Confirmado",
            Canceled = "Cancelado";

        public int Id { get; set; }
        [DisplayName("Status")]
        public string Name { get; set; }
    }
}