using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bestpricedaily.Models;
using bestpricedaily.Misc.Repository;
using AutoMapper;
using bestpricedaily.ViewModels;

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
        public async Task<IEnumerable<ItemViewModel>> Get()
        {
            var items = await _itemRepo.GetAll();
            return  _mapper.Map<ItemViewModel[]>(items);
        }

        // GET: api/Items/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ItemViewModel> ItemsGet(Guid id)
        {
            var item= await _itemRepo.GetById(id);
            return _mapper.Map<ItemViewModel>(item);
        }

        // POST: api/Items
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
