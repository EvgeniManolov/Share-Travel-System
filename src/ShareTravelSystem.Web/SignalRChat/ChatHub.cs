using Microsoft.AspNetCore.SignalR;
using ShareTravelSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareTravelSystem.Web.SignalRChat
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("NewMessage", new Message
            {
                User = this.Context.User.Identity.Name,
                Text = message,
            });
        }
    }

    public class Message
    {
        public string User { get; set; }

        public string  Text { get; set; }
    }
}
