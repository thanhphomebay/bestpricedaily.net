using bestpricedaily;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bestpricedaily.Paypal
{

    public static class Helper
    {

        public static async Task<string> getAccessToken(IHttpClientFactory clientFactory, MyStoreSetting config)
        {
            var encodedConsumerKey = HttpUtility.UrlEncode(config.Paypal_client_id); //.Get("Paypay_client_id"));//["Paypal:sandbox:CLIENT_ID"]);
            var encodedConsumerKeySecret = HttpUtility.UrlEncode(config.Paypal_client_secret);//["Paypal:sandbox:SECRET"]);
            var credential = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", encodedConsumerKey, encodedConsumerKeySecret)));

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(config.Paypal_access_token_url), //["Paypal:sandbox:ACCESS_TOKEN_URL"]),
                Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-language", "en_US");
            request.Headers.Add("cache-control", "no-cache");

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credential);
            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<PaypalAccessTokenResponse>(res);
                return json.access_token;
            }
            else
            {
                return null;
            }
        }
        
    }
}
