using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Call.Models;
using Microsoft.AspNet.SignalR;

namespace Call.hubs
{
    public class ChatHub:Hub
    {
        public void Send(int sender, int receiver, string Msg)
        {
            string s = ConnectionsData.GetKey(receiver.ToString());
            Clients.Others.receive(sender,s);
        }
    }
}