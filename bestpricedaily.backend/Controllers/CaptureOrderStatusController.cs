using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Repository;
using bestpricedaily.Models;
using bestpricedaily.ViewModels;
namespace bestpricedaily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptureOrderStatusController : ControllerBase
    {
        private readonly IAsyncRepository<Order> _orderRepo;
        public CaptureOrderStatusController(IAsyncRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }


        // GET: api/Orders/5
        [HttpGet("{orderid}", Name = "captureorderstatus")]
        public async Task<CaptureOrderStatusView> CaptureOrderStatus(string orderid)
        {
            var order = await _orderRepo.FirstOrDefaultAsync(x => x.order_id == orderid);
            // return new ContentResult
            // {
            //     ContentType = "html/text",
            //     StatusCode = (int)HttpStatusCode.OK,
            //     Content =   order.order_id 
            // };
            return new CaptureOrderStatusView{ order_id = order.order_id.ToString() };
        }

    }

}
