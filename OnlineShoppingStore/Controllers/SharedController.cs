using OnlineShopping.Entities;
using OnlineShopping.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class SharedController : Controller
    {
        SharedServirce service = new SharedServirce();

        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();

            List<object> picturesJason = new List<object>();
            var pictures = Request.Files;

            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var path = Server.MapPath("~/Contents/images/") + fileName;

                 picture.SaveAs(path);

                var dbPicture = new Picture();

                dbPicture.URL = fileName;

                int pictureID = service.SavePicture(dbPicture);

                picturesJason.Add(new { ID = pictureID, pictureURL = fileName });
            }
            result.Data = picturesJason;

            return result;
        }
    }
}