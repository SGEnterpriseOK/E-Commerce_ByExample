using OnlineShopping.Entities;
using OnlineShopping.Services;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Repository;
using OnlineShoppingStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AuctionsController : Controller
    {
        AuctionsService auctionService = new AuctionsService();

        CategoriesService categoriesService = new CategoriesService();

        SharedServirce sharedServirce = new SharedServirce();

        [HttpGet]
        public ActionResult AucIndex(int? categoryID, string searchTerm, int? pageNo)
        {
            AuctionsListingViewModel model = new AuctionsListingViewModel();

             model.PageTitle = "Online Auctions";
             model.PageDescription = "Auctions Listing Page";

            model.CategoryID = categoryID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo;

            model.Categories = categoriesService.GetAllCategories();

            return View(model);
           
        }

        public ActionResult Listing(int? categoryID, string searchTerm, int? pageNo)
        {
            AuctionsListingViewModel model = new AuctionsListingViewModel();

            var pageSize = 1;

            model.Auctions = auctionService.SearchAuctions(categoryID, searchTerm, pageNo, pageSize);

            var totalAuctions = auctionService.GetAuctionCount();
            model.Pager = new Pager(totalAuctions, pageNo, pageSize);

            return PartialView(model);
        }


        // GET: Auctions
        [HttpGet]
        public ActionResult Create()
        {
            CreateAuctionsViewModel model = new CreateAuctionsViewModel();

            model.Categories = categoriesService.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Create(CreateAuctionsViewModel model)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {

                Auction auction = new Auction();
                auction.Title = model.Title;
                auction.CategoryID = model.CategoryID;
                auction.Description = model.Description;
                auction.ActualAmount = model.ActualAmount;
                auction.StartingTime = model.StartingTime;
                auction.EndingTime = model.EndingTime;

                //LINQ
                if (!string.IsNullOrEmpty(model.AuctionPictures))
                {
                    var pictureIDs = model.AuctionPictures
                        .Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries)
                        .Select(ID => int.Parse(ID)).ToList();
                    auction.AuctionPictures = new List<AuctionPicture>();
                    auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
                }


                //foreach (var picID in pictureIDs)
                //{
                //    var auctionPicture = new AuctionPicture();
                //    auctionPicture.PictureID = picID;

                //    auction.AuctionPictures.Add(auctionPicture);

                //}

                auctionService.SaveAuction(auction);

                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false, Error = "Unable to Save Auctions.Please enter valid values" };
            }

            return result;
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {
            CreateAuctionsViewModel model = new CreateAuctionsViewModel();

            var auction = auctionService.GetAuctionByID(ID);

            model.ID = auction.ID;
            model.Title = auction.Title;
            model.CategoryID = auction.CategoryID;
            model.Description = auction.Description;
            model.ActualAmount = auction.ActualAmount;
            model.StartingTime = auction.StartingTime;
            model.EndingTime = auction.EndingTime;

            model.Categories = categoriesService.GetAllCategories();
            model.AuctionPicturesList = auction.AuctionPictures;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(CreateAuctionsViewModel model)
        {
            Auction auction = new Auction();
            auction.ID = model.ID;
            auction.Title = model.Title;
            auction.CategoryID = model.CategoryID;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartingTime = model.StartingTime;
            auction.EndingTime = model.EndingTime;

            if(!string.IsNullOrEmpty(model.AuctionPictures))
            {
                var pictureIDs = model.AuctionPictures
              .Split(new char[] {',' },StringSplitOptions.RemoveEmptyEntries)
              .Select(ID => int.Parse(ID)).ToList();

                auction.AuctionPictures = new List<AuctionPicture>();
                auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture { AuctionID = auction.ID, PictureID = x }).ToList());
            }
          
            auctionService.UpdateAuction(auction);

            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            
            auctionService.DeleteAuction(auction);         
            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            AuctionDetailsViewModel model = new AuctionDetailsViewModel();

            model.Auction = auctionService.GetAuctionByID(ID); 

            model.PageTitle = "Auctions Details: " + model.Auction.Title;  
            //model.PageDescription = model.Auction.Description.Substring(0,10);
            
            return View(model);
        }

    }
}