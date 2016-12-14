using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.DTOs
{
    public class TableDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}