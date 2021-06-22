using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace OnlineShoppingStore.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _UnitOfWork = new GenericUnitOfWork();
        dbMyOnlineShoppingEntities context = new dbMyOnlineShoppingEntities();

        public IEnumerable<Tbl_Product> ListOfProducts { get; set; }
        public IEnumerable<Tbl_Product> FeaturedProducts { get; set; }

        public HomeIndexViewModel CreateModel(string search)
        {
            SqlParameter[] param=new SqlParameter[] {
                new SqlParameter("@search", search?? (Object)DBNull.Value)

            };
            IEnumerable<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch", param).ToList();          ;
            return new HomeIndexViewModel()
                      
            {
                FeaturedProducts = data,
                ListOfProducts = data
            };
        }
    }
}