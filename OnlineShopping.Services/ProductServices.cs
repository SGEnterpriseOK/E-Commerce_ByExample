using OnlineShopping.Data;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Services
{
    public class ProductServices
    {
        public List<Products> GetAllProducts()
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Product.ToList();
        }

        public List<Products> SearchProducts(int? categoriesID, string searchTerm, int? pageNo, int pageSize)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

          var product = context.Product.AsQueryable();
          
            if(categoriesID.HasValue && categoriesID.Value > 0 )
            {
                product = product.Where(x => x.CategoriesID == categoriesID.Value);
            }
            if(!string.IsNullOrEmpty(searchTerm))
            {
                product = product.Where(x => x.ProductName.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;
            return product.OrderByDescending(x => x.CategoriesID).Skip(skipCount).Take(pageSize).ToList();

        }

        public List<Products> GetListOfProducts()
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Product.Take(10).ToList();

        }


        public Products GetProductsByID(int ID)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Product.Find(ID);
        }

        public void SaveProducts(Products products)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            context.Product.Add(products);

            context.SaveChanges();
        }

        public void UpdateProducts(Products products)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            context.Entry(products).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }


        public void DeleteProducts(Products products)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            context.Entry(products).State = System.Data.Entity.EntityState.Deleted;

            context.SaveChanges();
        }

        public int GetProductsCount()
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Product.Count();
        }
    }
}
