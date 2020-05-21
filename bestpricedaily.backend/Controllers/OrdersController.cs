using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Repository;
using bestpricedaily.Models;
using Core.ApiErrors;

namespace bestpricedaily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IAsyncRepository<Order> _orderRepo;
        public OrdersController(IAsyncRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }
        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderRepo.GetAll();
            if (orders != null)
                return Ok(orders);
            else
                return NotFound(new ApiError(404,"Orders are empty"));
        }
    }
}
