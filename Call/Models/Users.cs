using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Call.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gmail { get; set; }
        public string Password { get; set; }
        public string type { get; set; }
    }
}