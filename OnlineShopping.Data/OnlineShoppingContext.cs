using Microsoft.AspNet.Identity.EntityFramework;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Data
{
   public class OnlineShoppingContext : IdentityDbContext<OnlineShoppingUser>
    {
        public OnlineShoppingContext() : base("name=OnlineShoppingAuctions")
        {

        }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Products> Product { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<AuctionPicture> AuctionPictures { get; set; }


        public static OnlineShoppingContext Create()
        {
            return new OnlineShoppingContext();
        }
    }
}
