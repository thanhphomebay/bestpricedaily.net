using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using bestpricedaily.Hubs;
//using tst.Misc.Repository;
using bestpricedaily.Paypal;
using bestpricedaily.Models;
using bestpricedaily.Misc.Repository;
using Microsoft.Extensions.Options;
using bestpricedaily;

namespace bestpricedaily.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class CreateOrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        //private IConfigurationRoot _configRoot;
        //private IOptionsSnapshot<MyStoreSetting> _configRoot;
        private readonly MyStoreSetting _configRoot;
        private readonly IAsyncRepository<Item> _itemRepo;

        // public CreateOrderController(IHttpClientFactory client, IOptionsSnapshot<MyStoreSetting> configRoot, IHubContext<DataHub> hub, IAsyncRepository<Item> itemRepo)
        public CreateOrderController(IHttpClientFactory client, IOptionsSnapshot<MyStoreSetting> configRoot, IAsyncRepository<Item> itemRepo)
        {
            _clientFactory = client;
            _configRoot = configRoot.Value;
            _itemRepo = itemRepo;
        }

        [HttpGet]
        public async Task<string> Get()
        {

            //await  _hub.Clients. Invoke("SendMessage", "From backend server, done with paypal capture payment");
            // await _hub.Clients.All.SendAsync("cartcomponent", "From backend server, done with paypal capture payment");
            return "Get method";

        }

        // POST: api/CreateOrder
        [HttpPost]
        public async Task<string> createOrder([FromBody] ListItemBought bought)
        {
            try
            {
                var order = await makeOrder(bought.items);
                string bearerToken = await Paypal.Helper.getAccessToken(_clientFactory, _configRoot);
                //JsonSerializerSettings jss = new JsonSerializerSettings();
                //string strValue = JsonConvert.SerializeObject(order, jss);
                string stringOrder = JsonConvert.SerializeObject(order);

                //var x= Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(strOrder));

                //var stringContent = new StringContent(stringOrder);
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_configRoot.Paypal_create_payment_url),//["Paypal:sandbox:CREATE_PAYMENT_URL"]),
                    Content = new StringContent(stringOrder, Encoding.UTF8, "application/json") //noticed content-type is added with payload
                };
                //request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Accept-language", "en_US");
                request.Headers.Add("cache-control", "no-cache");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    return res;
                }
                else
                {
                    return response.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public async Task<PaypalOrder> makeOrder(ItemBought[] items)//, IConfigurationRoot config)
        {
            float taxRate = _configRoot.Tax;// float.Parse(_configRoot["OnlineStore:tax"]);
            float shipping = _configRoot.Shipping;// float.Parse(_configRoot["OnlineStore:shipping"]);
            float subTotal = 0;
            for (int i = 0; i < items.Length; i++)
            {
                var item = await _itemRepo.GetById(items[i].id);
                subTotal += items[i].quantity * item.price;
            }
            subTotal = float.Parse(String.Format("{0:0.00}", subTotal));
            float tax = float.Parse(String.Format("{0:0.00}", (taxRate * subTotal) / 100));

            float total = float.Parse(String.Format("{0:0.00}", tax + shipping + subTotal));


            PaypalOrder order = new PaypalOrder();
            order.intent = "CAPTURE".ToUpper();
            order.application_context = new PaypalOrderApplicationContext
            {
                brand_name = "bestpricedaily",
                return_url = string.Format(_configRoot.Paypal_success_url,_configRoot.BaseAddress),// _configRoot["Paypal:sandbox:RETURN_URL"];
                cancel_url = string.Format(_configRoot.Paypal_cancel_url,_configRoot.BaseAddress),// _configRoot["Paypal:sandbox:CANCEL_URL"];
                user_action = "CONTINUE",
            };

            order.purchase_units = new PaypalOrderPurchaseUnit[1];
            order.purchase_units[0] = new PaypalOrderPurchaseUnit();
            order.purchase_units[0].invoice_id = "BestPriceDaily.net " + Guid.NewGuid();
            order.purchase_units[0].amount = new PaypalOrderPurchaseUnitAmount();
            order.purchase_units[0].amount.currency_code = "USD";
            order.purchase_units[0].amount.value = total;
            order.purchase_units[0].amount.breakdown = new PaypalOrderPurchaseUnitAmountBreakdown();
            order.purchase_units[0].amount.breakdown.item_total = new UnitAmount();
            order.purchase_units[0].amount.breakdown.item_total.currency_code = "USD";
            order.purchase_units[0].amount.breakdown.item_total.value = subTotal;
            order.purchase_units[0].amount.breakdown.shipping = new UnitAmount();
            order.purchase_units[0].amount.breakdown.shipping.currency_code = "USD";
            order.purchase_units[0].amount.breakdown.shipping.value = shipping;
            order.purchase_units[0].amount.breakdown.tax_total = new UnitAmount();
            order.purchase_units[0].amount.breakdown.tax_total.currency_code = "USD";
            order.purchase_units[0].amount.breakdown.tax_total.value = tax;
            order.purchase_units[0].items = new PaypalOrderPurchaseUnitItem[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                var item = await _itemRepo.GetById(items[i].id);
                order.purchase_units[0].items[i] = new PaypalOrderPurchaseUnitItem
                {
                    name = item.name,
                    quantity = items[i].quantity,
                    sku = items[i].id.ToString(),
                    unit_amount = new UnitAmount
                    {
                        currency_code = "USD",
                        value = item.price,
                    }
                };
            }
            /*

            order.purchase_units[0].items[0] = new Item();
            order.purchase_units[0].items[0].name = "Kim Cuong";
            order.purchase_units[0].items[0].quantity = 1;
            order.purchase_units[0].items[0].sku = "123456";
            order.purchase_units[0].items[0].unit_amount = new UnitAmount();
            order.purchase_units[0].items[0].unit_amount.currency_code = "USD";
            order.purchase_units[0].items[0].unit_amount.value = 1;

            order.purchase_units[0].items[1] = new Item();
            order.purchase_units[0].items[1].name = "Ngoc Trai";
            order.purchase_units[0].items[1].quantity = 1;
            order.purchase_units[0].items[1].sku = "654321";
            order.purchase_units[0].items[1].unit_amount = new UnitAmount();
            order.purchase_units[0].items[1].unit_amount.currency_code = "USD";
            order.purchase_units[0].items[1].unit_amount.value = 1;
            */
            return order;
        }
    }
}
