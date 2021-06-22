using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingStore.Repository;

namespace OnlineShoppingStore.Models
{
    public class ProductDetails
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Minimum 3 and Minimum 5 and Maximum 100 characters are allowed", MinimumLength = 3)]
        [System.Web.Mvc.Remote("CheckProductExist", "Admin", ErrorMessage = "Product already exist")]
        public string ProductName { get; set; }

        [Required]
        [Range(1, 50)]
        public Nullable<int> CategoryId { get; set; }
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Required]
        public string Description { get; set; }
        public string ProductImage { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "1", "2000", ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }

        public bool IsFeatured { get; set; }

        public SelectList Categories { get; set; }

        internal void UploadImage(HttpPostedFileBase productImage, string v1, string v2, HttpServerUtilityBase server, GenericUnitOfWork unitOfWork, int v3, int productId, int v4)
        {
            throw new NotImplementedException();
        }
    }
}