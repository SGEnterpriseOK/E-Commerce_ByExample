using OnlineShopping.Entities;
using OnlineShopping.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Services
{
   public class CategoriesService
    {
        public List<Category> GetAllCategories()
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Categories.ToList();
        }

    }
}
