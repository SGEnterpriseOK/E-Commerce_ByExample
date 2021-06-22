using OnlineShopping.Data;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Services
{
   public class AuctionsService
    {
        public List<Auction> GetAllAuctions()
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Auctions.ToList();
        }

        public List<Auction> SearchAuctions(int? categoryID, string searchTerm, int? pageNo, int pageSize)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            var auctions = context.Auctions.AsQueryable();

            if (categoryID.HasValue && categoryID.Value > 0)
            {
                auctions = auctions.Where(x => x.CategoryID == categoryID.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                auctions = auctions.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;
            //pageNo = pageNo.HasValue ? pageNo.Value : 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            return auctions.OrderByDescending(x=>x.CategoryID).Skip(skipCount).Take(pageSize).ToList();
        }

        public List<Auction> GetPromotedAuctions()
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Auctions.Take(4).ToList();
        }

        public int GetAuctionCount()
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Auctions.Count();
        }

        public Auction GetAuctionByID(int ID)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            return context.Auctions.Find(ID);
        }

        public void SaveAuction(Auction auction)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            context.Auctions.Add(auction);

            context.SaveChanges();
        }

        public void UpdateAuction(Auction auction)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            context.Entry(auction).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }


        public void DeleteAuction(Auction auction)
        {
            OnlineShoppingContext context = new OnlineShoppingContext();

            context.Entry(auction).State = System.Data.Entity.EntityState.Deleted;

            context.SaveChanges();
        }


    }
}
