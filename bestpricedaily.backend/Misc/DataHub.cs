
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace bestpricedaily.Hubs
{
    public class DataHub : Hub
    {
        public async Task SendMessage(string msg)
        {
            await Clients.All.SendAsync("cartcomponent", msg);
        }
    }
}

