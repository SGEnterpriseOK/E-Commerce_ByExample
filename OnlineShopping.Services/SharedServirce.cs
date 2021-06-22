using OnlineShopping.Data;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Services
{
    public class SharedServirce
    {
        public int SavePicture(Picture picture)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            context.Pictures.Add(picture);

            context.SaveChanges();

            return picture.ID;
        }

        //public int SaveAucPic(AuctionPicture auctionPicture)
        //{
        //    OnlineShoppingContext context = new OnlineShoppingContext();

        //    context.AuctionPictures.Add(auctionPicture);

        //    context.SaveChanges();

        //    return auctionPicture.ID;
        //}
    }
}
