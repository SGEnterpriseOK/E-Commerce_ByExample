using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities
{
    public class Categories : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Products> Product { get; set; }
    }
}
