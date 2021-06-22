using OnlineShopping.Data;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Services
{
   public class CategoryServices
    {
        public List<Categories> GetAllCategory()
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Category.ToList();
        }
    }
}
