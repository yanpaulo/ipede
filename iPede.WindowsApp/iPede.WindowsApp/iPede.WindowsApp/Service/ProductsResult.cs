using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace iPede.WindowsApp.Service
{
    [DataContract]
    public class ProductsResult
    {
        public Product[] Products { get; set; }
    }
}
