using Microsoft.Ajax.Utilities;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLySinhVien.Extensions
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        //private readonly IUnitOfWork _unitOfWork;
        //public AuthorizeFilter(IUnitOfWork unitOfWork) 
        //{
        //    _unitOfWork = unitOfWork;
        //}
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var request = filterContext.HttpContext.Request;
            //var url = "/Authentication/login";
            //if (request.Url.AbsolutePath.ToLower() == url.ToLower() || request.Url.AbsolutePath.ToLower() == "/")
            //{
            //    base.OnActionExecuting(filterContext);
            //    return;
            //}

            //base.OnActionExecuting(filterContext);
        }
    }
}