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
    public class LoginFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            
 
            if (UserSession.User == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.Write("<div class='alert alert-danger'>You session has been expired, You must <a href='/login/?next="+ filterContext.HttpContext.Request.UrlReferrer + "'>login</a> again !</div>");
                    filterContext.HttpContext.Response.End();
                    return;
                }
                filterContext.Result = new HttpUnauthorizedResult();
                filterContext.HttpContext.Response.Redirect("/Users/Login/?next=" + filterContext.HttpContext.Request.RawUrl );
            }
            
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoginFilter());
        }
    }

    
}
