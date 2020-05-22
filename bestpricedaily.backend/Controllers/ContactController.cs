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
using Core.Repository;
using Microsoft.Extensions.Options;
using bestpricedaily.ViewModels;
using Microsoft.Extensions.Logging;

namespace bestpricedaily.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        //private IConfigurationRoot _configRoot;
        //private IOptionsSnapshot<MyStoreSetting> _configRoot;
        private readonly MyStoreSetting _configRoot;
        private readonly IAsyncRepository<Item> _itemRepo;

        // public CreateOrderController(IHttpClientFactory client, IOptionsSnapshot<MyStoreSetting> configRoot, IHubContext<DataHub> hub, IAsyncRepository<Item> itemRepo)
        private readonly ILogger _logger;

        // public CaptureOrderController(ILogger<CaptureOrderController> logger, IHttpClientFactory client, IOptionsSnapshot<MyStoreSetting> configRoot, IHubContext<DataHub> hub, IMapper mapper, IAsyncRepository<Order> orderRepo)
        public ContactController(ILogger<CaptureOrderController> logger, IHttpClientFactory client, IOptionsSnapshot<MyStoreSetting> configRoot, IAsyncRepository<Item> itemRepo)
        {
            _clientFactory = client;
            _configRoot = configRoot.Value;
            _itemRepo = itemRepo;
            _logger = logger;
        }

        // POST: api/CreateOrder
        [HttpPost]
        public async Task<IActionResult> Contact([FromBody] ContactViewModel contact)
        {
            try
            {
                if(contact.description.Length==0)
                    return Ok("empty message");
                var client = new System.Net.Mail.SmtpClient("localhost", 25);
                client.UseDefaultCredentials = false;
                // client.EnableSsl = true;

                // client.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
                _logger.LogDebug("contact from: " + contact.email);
                _logger.LogDebug("contact name: " + contact.name);
                var mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress(contact.email);

                _logger.LogDebug("pass mail message from");
                mailMessage.To.Add("contact@bestpricedaily.net");

                // if (!string.IsNullOrEmpty(email.Cc))
                // {
                //     mailMessage.CC.Add(email.Cc);
                // }

                mailMessage.Body = contact.description;

                mailMessage.Subject = "Questions/Concern from " + contact.name + " using contact form at BestPriceDaily.net";

                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

                _logger.LogDebug("About to send");
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }
    }
}