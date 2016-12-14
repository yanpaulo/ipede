using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class ModelContext
    {
        private static ModelContext _instance;

        private ModelContext()
        {
            
        }

        public static ModelContext Instance => _instance ?? (_instance = new ModelContext());

        //public Cart Cart { get; } = new Cart();

        public Table Table { get; set; }

        public Order ActiveOrder { get; set; }

        

    }
}
