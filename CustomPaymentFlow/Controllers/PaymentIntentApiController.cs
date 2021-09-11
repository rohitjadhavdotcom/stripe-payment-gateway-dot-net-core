using CustomPaymentFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPaymentFlow.Controllers
{
    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentIntentApiController : Controller
    {
        [HttpPost]
        [Route("create")]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
                Currency = "INR",
                Metadata = new Dictionary<string, string> {
                    {"Identifier","321"},
                    {"AdditiobalInfo","xyz"}
                }
            });
            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }
        [HttpPost]
        [Route("TransactionNote")]
        public ActionResult TransactionNote(TransactionResponse response)
        {
            var paymentIntents = new PaymentIntentService();
            var info = paymentIntents.Get(response.paymentIntentId);
            var metadata = info.Metadata.TryGetValue("Identifier", out string Identifier);
            var vustomerInfo = info.Charges.Data;
            if(vustomerInfo[0].Status == "succeeded")
            {
                return Json(true);

            }
            else
            {
                return Json(false);

            }
        }
        [NonAction]
        private int CalculateOrderAmount(Item[] items)
        {
            var price = (from i in new ViewModel().Items
                        join j in items
                        on i.Id equals j.Id
                        select i).Sum(i=>i.Price)*100;
            return price;
        }
    }
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
    public class TransactionResponse
    {
        public string paymentIntentId { get; set; }
    }
}
