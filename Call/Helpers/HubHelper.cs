using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Call.Helpers
{
    public class HubHelper
    {
        public string x;
        public string getKey(Dictionary<string,string> dict,string key)
        {
            foreach (var k in dict)
            {
                if (k.Value.Equals(key))
                {
                    x = k.Key;
                }
            }
            return x;
        }
    }
}