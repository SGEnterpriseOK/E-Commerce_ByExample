using OnlineShopping.Entities;
using OnlineShopping.Services;
using Newtonsoft.Json;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Repository; 
using OnlineShoppingStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _UnitOfWork = new GenericUnitOfWork();

        ProductServices productServices = new ProductServices();

        CategoryServices categoryServices = new CategoryServices();

        SharedServirce sharedService = new SharedServirce();

        [HttpGet]
        public ActionResult DashBoard(int? categoiesID, string searchTerm, int? pageNo)
        {
            ProductListingViewModel model = new ProductListingViewModel();

            //model.PageTitle = "Farmer's Choice Products";
            //model.PageDescription = "Products Listing Page";

            model.CategoriesID = categoiesID;
            model.SearchTerm = searchTerm;
            model.PageNo = pageNo;
            
            return View(model);

        }

        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _UnitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories); 
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }



       public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
                if(categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_UnitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);
        }

        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {
            _UnitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }


        [HttpGet]
        public ActionResult Product(int? categoriesID, string searchTerm, int? pageNo)
        {
            var PageSize = 2;

            ProductListingViewModel model = new ProductListingViewModel();

            model.Product = productServices.SearchProducts(categoriesID, searchTerm, pageNo, PageSize);

            var totalProducts = productServices.GetProductsCount();

            model.Pager = new Pager(totalProducts, pageNo, PageSize);
         
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ProductAdd()
        {
            CreateProductsViewModel model = new CreateProductsViewModel();
            model.Category = categoryServices.GetAllCategory();

            return View(model);
        }

        [HttpPost]
        public ActionResult ProductAdd(CreateProductsViewModel model)
        {
            Products product = new Products();

            product.ProductName = model.ProductName;
            product.CategoriesID = model.CategoriesID;
            product.IsActive = model.IsActive;
            product.IsDelete = model.IsDelete;
            product.CreatedDate = model.CreatedDate;
            product.Description = model.Description;
            product.IsFeatured = model.IsFeatured;
            product.Quantity = model.Quantity;
            product.Price = model.Price;

            if(!string.IsNullOrEmpty(model.ProductPictures))
            {
                var pictureIDs = model.ProductPictures
                    .Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)
                    .Select(ID => int.Parse(ID)).ToList();
                product.ProductPictures = new List<ProductPicture>();
                product.ProductPictures.AddRange(pictureIDs.Select(x => new ProductPicture() { PictureID = x }).ToList());
            }
  
            //foreach (var picID in pictureIDs)
            //{
            //    var productPicture = new ProductPicture();
            //    productPicture.PictureID = picID;

            //    product.ProductPictures.Add(productPicture);
            //}

            productServices.SaveProducts(product);

            return RedirectToAction("Product");

        }


        public ActionResult ProductEdit(int productId)
        {
            
            return View(_UnitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {

                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                //file is uploaded
                file.SaveAs(path);

            }
            tbl.ProductImage = file != null ? pic: tbl.ProductImage;
            tbl.ModifiedDate = DateTime.Now;
            _UnitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
            return RedirectToAction("Product");
        } 

        public ActionResult ProductDetail(int productId)
        {
            Tbl_Product pd = _UnitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(productId);
            return View(pd);
            
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(ProductDetails pd,  HttpPostedFileBase _ProductImage)
        {
            if (ModelState.IsValid)
            {
                Tbl_Product prod = _UnitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(pd.ProductId);
                prod = prod != null ? prod : new Tbl_Product();
                prod.CategoryId = pd.CategoryId;
                prod.Description = pd.Description;
                prod.IsActive = pd.IsActive;
                prod.IsFeatured = pd.IsFeatured;
                prod.Price = pd.Price;
                prod.ProductImage = _ProductImage != null ? _ProductImage.FileName : prod.ProductImage;
                prod.ProductName = pd.ProductName;
                prod.ModifiedDate = DateTime.Now;

                if (prod.ProductId == 0)
                {
                    prod.CreatedDate = DateTime.Now;
                    prod.IsDelete = false;
                    _UnitOfWork.GetRepositoryInstance<Tbl_Product>().add(prod);
                }
                else
                {
                    _UnitOfWork.GetRepositoryInstance<Tbl_Product>().Update(prod);
                    _UnitOfWork.SaveChanges();
                }

                if (_ProductImage != null)
                    pd.UploadImage(_ProductImage, prod.ProductId + "_", "~/ProductImg/", Server, _UnitOfWork, 0, prod.ProductId, 0);
                return RedirectToAction("Products");
            }

            pd.Categories = new SelectList(_UnitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable(), "CategoryId", "CategoryName");
            return View("UpdateProduct", pd);
        }

        public JsonResult CheckProductExist(string ProductName)
        {
            int productId = 0;
            if (HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["productId"] != null)
                productId = Convert.ToInt32(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["productId"]);
            var productExist = _UnitOfWork.GetRepositoryInstance<OnlineShoppingStore.DAL.Tbl_Product>().GetAllRecordsIQueryable().Where(i => i.ProductName == ProductName && i.ProductId != productId && i.IsActive == true && i.IsDelete == false).Count();
            return productExist == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}