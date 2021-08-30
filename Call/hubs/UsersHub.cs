using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Call.Models;
using Call.Controllers.Api;

namespace Call.hubs
{
    public class UsersHub : Hub
    {
        public UsersController userObj = new UsersController();
    

        public void NewUser(Users user)
        {
            if (userObj.AddUser(user))
            {
                Clients.Others.newUser(user);
                Clients.Client(Context.ConnectionId).newUser(true);
            }
            else
            {
                Clients.Client(Context.ConnectionId).newUser(false);
            }

        }

        public void userLogin(string id)
        {
            ConnectionsData.Ids.Add(Context.ConnectionId,id);
            Clients.Others.connectedUser(id);
        }
        
        public void OnlineUsers()
        {
            Clients.Client(Context.ConnectionId).onlineUsers(ConnectionsData.Ids);
        }
       
    }
}