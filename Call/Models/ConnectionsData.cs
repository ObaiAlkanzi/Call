using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Call.Models
{
    public static class ConnectionsData
    {
        //public static List<string> Ids = new List<string>();
        
        public static Dictionary<string, string> Ids = new Dictionary<string, string>();

        public static string GetKey(string value)
        {
            if (ConnectionsData.Ids.ContainsValue(value))
            {
                return "ok";
            }
            else
            {
                return "connection id not found";
            }
        }
    }

    
}