using System.Net;
using bestpricedaily;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bestpricedaily.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CancelOrderController : Controller
    {
        // GET: /<controller>/
        private readonly MyStoreSetting _configRoot;

        public CancelOrderController(IOptionsSnapshot<MyStoreSetting> configRoot)
        {
            _configRoot = configRoot.Value;
        }
        public ContentResult Index([FromQuery] string token)
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><head><script>(function(){ window.location.href=\"" + _configRoot.BaseAddress + "\"})()</script></head><body></body></html>"
                //Content = "<html><body><div style=\"text-align:center\"><h3>Your payment with Paypal is completed. </h3><a href=\"http://192.168.1.150:4200/\"><button>Back to Store</button></a><br></body></html>"

            };
        }
    }
}
