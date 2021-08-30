using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Call.Models;

namespace Call
{
    public class ServerHub:Hub
    {
        public override Task OnDisconnected(bool stopCalled)
        {
            string id = ConnectionsData.Ids[Context.ConnectionId];
            ConnectionsData.Ids.Remove(Context.ConnectionId);
           
            Clients.Others.disconnected(id);
            return base.OnDisconnected(stopCalled);
        }

    }
}