using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dark.Hubs
{
    public class LogHub : Hub
    {
        public async Task SendMessage(string server, string log)
        {
            await Clients.All.SendAsync("ReceiveMessage", server, log);
        }
    }
}
