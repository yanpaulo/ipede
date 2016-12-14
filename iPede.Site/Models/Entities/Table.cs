using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class Table
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}