using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Call.Models;

namespace Call.Controllers
{
    public class HomeController : Controller
    {
        private string pointer;
        private bool IdState;
        public Notification noti = new Notification();
        public ActionResult Index()
        {
            if (Session["name"] == null || Session["password"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();               
            }
        }

        public ActionResult NewUser(Users user)
        {
            try
            {
                user.type = "1";
                user.Path = "/Profiles/"+user.Profile.FileName;
                user.Profile.SaveAs(Server.MapPath("~") + user.Path);
                string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand query = new SqlCommand(String.Format("insert into Users values ('{0}','{1}','{2}','{3}','{4}')", user.Name, user.Gmail, user.Password, user.type, user.Profile.FileName), conn);
                query.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction("Login", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Register","Home");
            }
            
            
        }
        public ActionResult Register()
        {
            ViewBag.Message = "Your application description page.";

            if (Session["name"] == null || Session["password"] == null)
            {
                return View();
            }
            else
            {

                if (Session["type"].Equals("1"))
                {
                    pointer = "Chat";

                }
                else if (Session["type"].Equals("3"))
                {
                    pointer = "Index";
                }
                return RedirectToAction(pointer, "Home");
            }
        }
        public ActionResult Login(Notification noti)
        {
            if (Session["name"] == null || Session["password"] == null)
            {
                return View(noti);
            }
            else
            {
                
                if(Session["type"].Equals("1"))
                {
                    pointer = "Chat";

                }
                else if(Session["type"].Equals("3"))
                {
                    pointer = "Index";
                }
                return RedirectToAction(pointer, "Home");
            }
            
        }
        public ActionResult Chat()
        {
            if (Session["name"] == null || Session["password"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult LoginCheck(string name,string password)
        {
            /*
             * get database connection.
             * [1] check if user is found.
             * [2] check if the id already connected if not connected go to 3.
             * [3] add the id it LoginData.userData and save sessions.
             * [4] specifing user's type , if 1 go to chat view else if 3 go to Index view
             * [5] to get the count of connected ids LoginData.userData.Count().
             **/
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter query = new SqlDataAdapter(String.Format("select * from Users where Name = '{0}' and Password = '{1}' ",name,password),conn);
            DataTable dt = new DataTable();
            query.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (CheckUserid(dt.Rows[0]["Id"].ToString()))
                {
                    LoginData.userData.Add(dt.Rows[0]["Id"].ToString());
                    Session["name"] = name;
                    Session["password"] = password;
                    Session["type"] = dt.Rows[0]["Type"].ToString();
                    Session["id"] = dt.Rows[0]["Id"].ToString();
                    if (dt.Rows[0]["Type"].ToString().Equals("1")){
                        pointer = "Index";
                    }
                    else {
                        pointer = "Index";
                    }
                    return RedirectToAction(pointer, "Home");
                }
                else
                {
                    noti.Header = "failed";
                    noti.Msg = "already connected in another device";
                    return RedirectToAction("Login", "Home", noti);
                }
            }
            else
            {
                noti.Header = "failed";
                noti.Msg = "invalid name or password";
                return RedirectToAction("Login", "Home", noti);
            }
        }

        private bool CheckUserid(string userId)
        {
            IdState = true;
            foreach (var item in LoginData.userData)
            {
                if (item.Equals(userId))
                {
                    IdState = false;
                    break;
                }
                
            }
            return IdState;
        }
        
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Login","Home");
        }


    }
}