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
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter query = new SqlDataAdapter(String.Format("select * from Users where Name = '{0}' and Password = '{1}' ",name,password),conn);
            DataTable dt = new DataTable();
            query.Fill(dt);
            if (dt.Rows.Count > 0) {
                
                Session["name"] = name;
                Session["password"] = password;
                Session["type"] = dt.Rows[0]["Type"].ToString();
                Session["id"] = dt.Rows[0]["Id"].ToString();
                string pointer;
                if (dt.Rows[0]["Type"].ToString().Equals("1"))
                {
                    // pointer = "Chat";
                    pointer = "Index";

                }
                else
                {
                    pointer = "Index";
                }

                return RedirectToAction(pointer, "Home");
            }
            else
            {
                Notification noti = new Notification();
                noti.Header = "failed";
                noti.Msg = "invalid name or password";
                return RedirectToAction("Login", "Home",noti);
            }
        }

        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Login","Home");
        }


    }
}