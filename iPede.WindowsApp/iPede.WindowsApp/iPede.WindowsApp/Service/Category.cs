using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPede.WindowsApp.Service
{
    public class Category
    {
        public int Id { get; set; }

        public int? ParentCategoryId { get; set; }

        public string Name { get; set; }

        public string ParentCategoryName { get; set; }

        public virtual IEnumerable<Category> SubCategories { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
