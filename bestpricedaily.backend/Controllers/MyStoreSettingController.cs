using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bestpricedaily.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        public MyStoreSettingView Get()
        {
            return new MyStoreSettingView { tax_rate= _settings.Tax, shipping_base_rate = _settings.Shipping };
        }
    }
}
