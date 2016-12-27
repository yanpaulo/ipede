using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class Table
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ObservableCollection<Order> Orders { get; set; }
    }
}
