using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bestpricedaily.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Core.ApiErrors;

namespace bestpricedaily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyStoreSettingController : ControllerBase
    {
        private readonly MyStoreSetting _settings;
        public MyStoreSettingController(IOptionsSnapshot<MyStoreSetting> settings)
        {
            _settings = settings.Value;
        }
        // GET: api/MyAppSetting
        [HttpGet]
        public IActionResult Get()
        {
            if (_settings != null)
                return Ok(new MyStoreSettingView { tax_rate = _settings.Tax, shipping_base_rate = _settings.Shipping });
            else
                return NotFound(new ApiError(404,"Store settings not found"));
        }
    }
}
