using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities
{
    public class Products : BaseEntity
    {
        public int ProductID { get; set; }
        //for foreign key
        public virtual Categories Categories { get; set; }
        public int CategoriesID { get; set; }

        public string ProductName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }

        public virtual List<ProductPicture> ProductPictures { get; set; }

    }
}
