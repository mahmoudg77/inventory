using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.Threading.Tasks;

namespace Inventory
{

    public class UserSession
    {
        public static User User {
            get
            {
                if (HttpContext.Current.Request.Cookies["X-SMARTSTOCK-SESSIONID"] != null && HttpContext.Current.Session["SESSION_ID"]==null)
                {
                    HttpContext.Current.Session.Add("SESSION_ID", HttpContext.Current.Request.Cookies["X-SMARTSTOCK-SESSIONID"].Value);
                }

                if (HttpContext.Current.Session["SESSION_ID"] == null) return null;

                int sessionid=0;
                int.TryParse(HttpContext.Current.Session["SESSION_ID"].ToString(),out sessionid);
                if (sessionid == 0) return null;

                using (var db=new InventoryEntities())
                {
                    var u= db.Users.Where(a=>a.Usr_Id== sessionid).First();
                    return u;
                }
            }
        }

        
        public static async Task<User> Login(string Email,string Password, bool rem = false)
        {
            
            using (var ctx=new InventoryEntities())
            {

                User u = ctx.Users.Where(a => a.Email == Email && a.Password==Password).FirstOrDefault();
                 

                if (u == null) return null;

 
                HttpContext.Current.Response.Cookies.Clear();

                if (rem)
                {
                    var cookie = new HttpCookie("X-SMARTSTOCK-SESSIONID", u.Usr_Id.ToString());
                    cookie.Expires=DateTime.Now.AddDays(5);
                   
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                HttpContext.Current.Session.Add("SESSION_ID", u.Usr_Id);

                return u;
            }
        }
        public static bool Logout()
        {
            User session = User;
            if (session == null)
                return true;

            

            HttpContext.Current.Response.Cookies.Clear();
            HttpContext.Current.Session.Clear();


            return true;
        }
         

    }
}