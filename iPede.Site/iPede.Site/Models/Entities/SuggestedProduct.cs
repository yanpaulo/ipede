using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class SuggestedProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int OrderingNumber { get; set; }
    }
}