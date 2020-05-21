using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bestpricedaily.Models;
using Core.Repository;
using AutoMapper;
using bestpricedaily.ViewModels;
using Core.ApiErrors;

namespace bestpricedaily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Item> _itemRepo;
        public ItemsController(IMapper mapper, IAsyncRepository<Item> itemRepo)
        {
            _mapper = mapper;
            _itemRepo = itemRepo;
        }


        // GET: api/Items
        [HttpGet]
        public async Task<IActionResult> GetAction()
        {
            // throw new Exception("Exception while fetching all t");

            var items = await _itemRepo.GetAll();
            if (items != null)
                return Ok(items);
            else
                return NotFound(new ApiError(404, "Items are not found"));
        }

        // GET: api/Items/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> ItemsGet(Guid id)
        {
            var item = await _itemRepo.GetById(id);
            if (item != null)
                return Ok(_mapper.Map<ItemViewModel>(item));
            else
                return NotFound(new ApiError(404,"Item is not found")); 
        }
    }
}
