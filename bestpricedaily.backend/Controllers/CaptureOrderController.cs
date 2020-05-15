using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using bestpricedaily;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuickType;
using bestpricedaily.Hubs;
using bestpricedaily.Misc.Repository;
using bestpricedaily.Models;
using bestpricedaily.Paypal;
using Microsoft.Extensions.Logging;

namespace bestpricedaily.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaptureOrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        // private readonly IHubContext<DataHub> _hub;
        //private IConfigurationRoot _configRoot;
        private readonly MyStoreSetting _configRoot;
        private readonly IAsyncRepository<Order> _orderRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        // public CaptureOrderController(ILogger<CaptureOrderController> logger, IHttpClientFactory client, IOptionsSnapshot<MyStoreSetting> configRoot, IHubContext<DataHub> hub, IMapper mapper, IAsyncRepository<Order> orderRepo)
        public CaptureOrderController(ILogger<CaptureOrderController> logger, IHttpClientFactory client, IOptionsSnapshot<MyStoreSetting> configRoot, IMapper mapper, IAsyncRepository<Order> orderRepo)
        {
            _clientFactory = client;
            _configRoot = configRoot.Value;
            // _hub = hub;
            _mapper = mapper;
            _orderRepo = orderRepo;
            _logger = logger;
        }
        // GET: Success
        //success? token = 1TV44543B6260131T&PayerID=CSGZPMFTWZ6Y4
        /*[HttpGet("{token}/{payerid}")]
        public string Get(string token, string payerid)
        {
            return $"{token}:{payerid}";
        }*/

        [HttpGet]
        public async Task<ContentResult> CaptureOrder([FromQuery]string token, [FromQuery]string payerid)
        {
            //return $"{token}:{payerid}";
            string bearerToken = await Helper.getAccessToken(_clientFactory, _configRoot);
            String uri = string.Format(_configRoot.Paypal_capture_payment_url,token);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                // RequestUri = new Uri($"https://api.sandbox.paypal.com/v2/checkout/orders/{token}/capture"),
                RequestUri = new Uri(uri),

                //this really suck, took me a whole day => HttpRequestMessage prevented me add content-type for httpclient post with NO content.
                //I tried all => only work with empty string 
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };




            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var client = _clientFactory.CreateClient();

            _logger.LogInformation("About to Request capture order");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Done request capture order");
                var res = await response.Content.ReadAsStringAsync();


               // await _hub.Clients.All.SendAsync("cartcomponent", "From backend server, done with paypal capture payment");

                var capturedOrder = QuickType.OrderViewModel.FromJson(res);

                var order = _mapper.Map<Order>(capturedOrder);

                await _orderRepo.Add(order);

                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    //window.localStorage.clear();
                    //Content = "<html><head><script>(function(){window.close()})()</script></head><body></body></html>"
                    Content = $"<html><body><head><script></script></head><div style=\"text-align:center\"><h3>Your payment with Paypal is completed. </h3><a href=\"{_configRoot.BaseAddress}\"><button>Back to Store</button></a><br></body></html>"
                };
            } 
            else
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    //Content = "<html><head><script>(function(){window.close()})()</script></head><body></body></html>"
                    Content = response.ToString()
                };
            }

        }
    }
}