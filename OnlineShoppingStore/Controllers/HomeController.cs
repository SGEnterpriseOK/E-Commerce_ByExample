using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models.Home;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using OnlineShoppingStore.ViewModels;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Services;

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        dbMyOnlineShoppingEntities ctx = new dbMyOnlineShoppingEntities();

        public GenericUnitOfWork _UnitOfWork = new GenericUnitOfWork();

        AuctionsService service = new AuctionsService();

        ProductServices services = new ProductServices();

        public ActionResult HomeAuctions()
        {
            AuctionsViewModel vModel = new AuctionsViewModel();

           // vModel.PageTitle = "Farmer's Choice Auctions";
           // vModel.PageDescription = "Auctions Home";

            vModel.AllAuctions = service.GetAllAuctions();
            vModel.PromotedAuctions = service.GetPromotedAuctions();

            return View(vModel);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult Index()
        {
            ProductsViewModel model = new ProductsViewModel();

            model.AllProducts = services.GetAllProducts();
            model.ListOfProducts = services.GetListOfProducts();
            //var AllProducts = services.GetAllProducts();

        
            return View(model);
            //HomeIndexViewModel model = new HomeIndexViewModel();
            //ViewBag.FeaturedProducts = _UnitOfWork.GetRepositoryInstance<Tbl_Product>().GetListByParameter(i => i.IsFeatured == true).ToList();
            //return View(model.CreateModel(search));

        }

        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>) Session["cart"];
                var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            }) ;
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }

        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Tbl_Product.Find(productId);
                for (int i = 0; i < count;i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });

                        }
                    }
                }
               
                Session["cart"] = cart;
            }

            return Redirect("Index");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //var product = ctx.Tbl_Product.Find(productId)
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
                Session["cart"] = cart;

            }
            return Redirect("Index");


        }
    }
}