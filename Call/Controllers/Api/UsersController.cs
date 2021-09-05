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
    public class UsersController : ApiController
    {
        public string connStr;
        public SqlConnection conn;
        public UsersController()
        {
            connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conn = new SqlConnection(connStr);
        }

        [HttpPost]
        public bool AddUser(Users user)
        {
            try
            {
                conn.Open();
                SqlCommand query = new SqlCommand(String.Format("insert into Users values ('{0}','{1}','{2}','{3}')", user.Name, user.Gmail, user.Password, user.type), conn);
                query.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpGet]
        public DataSet GetUsers()
        {
            conn.Open();
            SqlDataAdapter query = new SqlDataAdapter("select * from Users",conn);
            DataSet ds = new DataSet();
            query.Fill(ds);
            conn.Close();
            return ds;
        }

        [HttpGet]
        public DataSet GetUsersById(int id)
        {
            conn.Open();
            SqlDataAdapter query = new SqlDataAdapter(String.Format("select name from Users where id = {0}",id), conn);
            DataSet ds = new DataSet();
            query.Fill(ds);
            conn.Close();
            return ds;
        }
    }
}
