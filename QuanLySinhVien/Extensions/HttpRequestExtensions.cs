using System.Web;
using System.Web.Security;


namespace QuanLySinhVien.Extensions
{
    public static class HttpRequestExtensions
    {
        public static FormsAuthenticationTicket GetAuthTicket(this HttpRequestBase request)
        {
            try
            {
                var formsAuthCookie = request.Cookies[FormsAuthentication.FormsCookieName];
                if (formsAuthCookie == null || string.IsNullOrEmpty(formsAuthCookie.Value))
                {
                    return new FormsAuthenticationTicket("", false, 0);
                }
                return FormsAuthentication.Decrypt(formsAuthCookie.Value);
            }    
            catch
            { }
            return new FormsAuthenticationTicket("", false, 0);
        }
    }
}