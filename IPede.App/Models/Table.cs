using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class Table
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public virtual IList<Order> Orders { get; set; }
    }
}
