using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace QuanLySinhVien.Helper
{
    public class AuthExpiredModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(OnEndRequest);
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpResponse response = app.Context.Response;
            HttpRequest request = app.Context.Request;

            if (request.IsAuthenticated)
            {
                HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    if (authTicket != null && authTicket.Expiration >= DateTime.Now)
                    {
                        //response.Redirect("~/");
                    }
                    else
                    {
                        response.ClearContent();
                        response.Redirect("~/Account/Login");
                    }
                }
            }
        }


        public void Dispose()
        {
            // implement IDisposable
        }
    }
}