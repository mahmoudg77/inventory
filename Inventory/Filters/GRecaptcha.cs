using Inventory.Models;
using Inventory.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Inventory
{
    public class GRecaptcha : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            var key =ConfigurationManager.AppSettings.Get("recaptcha_secret_key");

            bool isValid = false;
            if (!string.IsNullOrEmpty(key))
            {
                var recaptcha=filterContext.HttpContext.Request.Form.Get("g-recaptcha-response");
                if (!string.IsNullOrEmpty(recaptcha))
                {
                    using (var client = new HttpClient())
                    {

                        var uri = new Uri("https://www.google.com/recaptcha/api/siteverify");
                        //client.PostAsync()
                        var content = new FormUrlEncodedContent(new[]
                                    {
                                        new KeyValuePair<string, string>("secret",key ),
                                        new KeyValuePair<string, string>("response",recaptcha )
                                    });

                        var response = client.PostAsync(uri, content).Result;

                        string textResult = response.Content.ReadAsStringAsync().Result;

                        var res = JsonConvert.DeserializeObject<RecaptchaResponse>(textResult);
                        isValid = res.success;
                    }

                }
            }
           

            if (!isValid)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 422;
                    filterContext.HttpContext.Response.Write("<div class='alert alert-danger'>Recaptcha Validation Error !</div>");
                    filterContext.HttpContext.Response.End();
                    return;
                }
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode=422;
                filterContext.HttpContext.AddError(new Exception("Recaptcha Validation Error !",null));
                filterContext.HttpContext.Response.Redirect(filterContext.HttpContext.Request.RawUrl +"?error="+ "Recaptcha Validation Error !", true );
                filterContext.HttpContext.Response.End();
                return;


            }

        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    
}
