using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Inventory
{

    public class RoleFilter : FilterAttribute, IAuthorizationFilter
    {
        public int[] roles { get; set; }

        private int[] allRoles = new int[] { 1, 2, 3 };
 
        public RoleFilter(int[] roles)
        {
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {

            if (UserSession.User == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.Write("<div class='alert alert-danger'>You session has been expired, You must <a href='/login/?next=" + filterContext.HttpContext.Request.UrlReferrer + "'>login</a> again !</div>");
                    filterContext.HttpContext.Response.End();
                    return;
                }
                filterContext.Result = new HttpUnauthorizedResult();
                filterContext.HttpContext.Response.Redirect("/Users/Login/?next=" + filterContext.HttpContext.Request.RawUrl);
                filterContext.HttpContext.Response.End();
                return;
            }



            var u = UserSession.User;

            if (!this.roles.ToList().Contains(u.Usr_Type))
            {
                filterContext.Result = new HttpUnauthorizedResult();
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.ContentType = "application/json";
                    filterContext.HttpContext.Response.Write("{\"type\":\"error\",\"message\":\"You are not authorized to do this action !!\"}");
                    //filterContext.HttpContext.Response.End();
                }
                else
                {

                    filterContext.HttpContext.Response.Redirect("/NotAuthorized");


                }

                return;
            }
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoginFilter());
        }
    }

}
