using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class PaymentsController : Controller
    {
        // GET: Payments
        public ActionResult PaymentWithPapal()
        {
            APIContext apicontext = PaypalConfiguration.GetAPIContext();
            try
            {
                string PayerId = Request.Params["PayerId"];
                if (string.IsNullOrEmpty(PayerId))
                {
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + 
                        "PaymentWithPapal/PaymentWithPapal?";

                    var Guid = Convert.ToString((new Random()).Next(1000000));
                    var createPayment = this.CreatePayment(apicontext, baseUri + "guid=" + Guid);

                    List<Links>.Enumerator links = createPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;

                    while(links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if(lnk.rel.ToLower().Trim().Equals("Approval_url"))
                        {
                            return View("FailureView");
                        }
                    }
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePaymnt = ExecutePayment(apicontext, PayerId, Session[guid] as string);

                    if(executePaymnt.ToString().ToLower()!="Approved")
                    {
                        return View("FailureView");
                    }
                }
            }
           catch (Exception)
            {
                
            }
            return View("SuccessView");
        }

    
        
        private object ExecutePayment(APIContext apicontext, string payerId, string PaymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = PaymentId };
            return this.payment.Execute(apicontext, paymentExecution);
        }

        private PayPal.Api.Payment payment;

        private Payment CreatePayment(APIContext apicontext, string redirectUrl)
        {
            var ItemList = new ItemList() { items = new List<Item>() };

            if (Session["cart"]!="")
            {

                List<Models.Home.Item> cart = (List<Models.Home.Item>)Session["cart"];
                foreach (var item in cart)
                {
                    ItemList.items.Add(new Item()
                    {
                        name = item.Product.ProductName.ToString(),
                        currency = "ZAR",
                        price = item.Product.Price.ToString(),
                        quantity = item.Product.Quantity.ToString(),
                        sku = "sku"
                    });
                }

                var payer = new Payer() { payment_method = "paypal" };

                var redirtUrl = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "Cancel=true",
                    return_url = redirectUrl
                };
                var details = new Details()
                {
                    tax = "1",
                    shipping = "1",
                    subtotal = "1"
                };

                var amount = new Amount()
                {
                    currency = "USD",

                    total = Session["SesTotal"].ToString(),
                    details = details

                };

                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = "#100000",
                    amount = amount,
                    item_list = ItemList
                });

                this.payment=new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirtUrl

                };

            }
            return this.payment.Create(apicontext);
           
           
        }
    }
}