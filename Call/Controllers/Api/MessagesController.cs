using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Call.Models;

namespace Call.Controllers.Api
{
    public class MessagesController : ApiController
    {
        public string connStr;
        public SqlConnection conn;
        public MessagesController()
        {
            connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conn = new SqlConnection(connStr);
        }

        [HttpPost]
        public int AddMsg(Messages message)
        {
            return message.Id;
            
        }
        [HttpGet]
        public string ShowMsg(string searchkey ,string searchValue)
        {
            return string.Format("key : {0} and value : {1} ",searchkey,searchValue);

        }
        [HttpGet]
        public string SearchById(int Id)
        {
            return string.Format(" search by id {0}",Id);

        }
    }
}
