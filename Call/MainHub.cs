using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Call.Models;
using Call.Helpers;
using System.Data;
using Call.Controllers.Api;

namespace Call
{
    public class MainHub:Hub
    {
        public string[] msg = new string[2];
        public HubHelper helper = new HubHelper();

        /*
         * [1] login Method - when client login
         */
        public void Login(string id)
        {
            
            if (ConnectionsData.Ids.ContainsValue(id))
            {
                string k = helper.getKey(ConnectionsData.Ids,id);
                Clients.Client(k).unauotherisze("another used connected");
            }
            else
            {
                ConnectionsData.Ids.Add(Context.ConnectionId,id);
                /*
                 * Get users information from the api
                 * get user's info int.Parse(id)
                 * UsersController users = new UsersController();
                    DataSet ds = users.GetUsers();
                 */

                Clients.Client(Context.ConnectionId).login(ConnectionsData.Ids);
                Clients.Others.newClientConnection(id);
            }
            
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string Id = ConnectionsData.Ids[Context.ConnectionId];
            Clients.Others.disconnectedClient(Id);
            ConnectionsData.Ids.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}