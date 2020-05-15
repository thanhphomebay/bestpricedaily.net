using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bestpricedaily.Misc.Repository;
using bestpricedaily.Models;

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
        public async Task<IEnumerable<Order>> Get()
        {
            return await _orderRepo.GetAll();
        }
    }
}
