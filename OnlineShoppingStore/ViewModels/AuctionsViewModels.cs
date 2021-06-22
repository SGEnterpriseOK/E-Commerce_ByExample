using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.ViewModels
{
    public class AuctionsListingViewModel : PageViewModel
    {
        public List<Auction> Auctions { get; set; }
        public int? CategoryID { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
        public int? PageNo { get; internal set; }

        public List<Category> Categories { get; set; }
    }

    public class ProductListingViewModel : PageViewModel
    {
        public List<Products> Product { get; set; }
        public int? CategoriesID  { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
        public int? PageNo { get; internal set; }
    }


    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }

        public List<Auction> PromotedAuctions { get; set; }
    }

    public class AuctionDetailsViewModel : PageViewModel
    {
        public Auction Auction { get; set; }
    }

        public class ProductsViewModel : PageViewModel
    {
        public List<Products> AllProducts { get; set; }

        public List<Products> ListOfProducts{ get; set; }

    }

    public class CreateProductsViewModel 
    {
        public virtual Categories Categories { get; set; }
        public int CategoriesID { get; set; }
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ProductPictures { get; set; }

        public List<Categories> Category { get; set; }
        public int ID { get; set; }
        public virtual List<ProductPicture> ProductPicturesList { get; set; }
        

    }

    public class CreateAuctionsViewModel : PageViewModel
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }

        [Required]
        [MinLength(15, ErrorMessage = "Minimum Length should be 15 characters.")]
        [MaxLength(150)] //nvarchar 150
        public string Title { get; set; }

        public string Description { get; set; }


        [Required]
        [Range(100, 1000000, ErrorMessage = "Actual Amount must be within 100 - 1000000")]
        public decimal ActualAmount { get; set; }

        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }

        public string AuctionPictures { get; set; }

        //public List<AuctionPicture> AuctionPictures { get; set; }
        public List<Category> Categories { get; set; }
        public List<AuctionPicture> AuctionPicturesList { get; set; }

    }
}