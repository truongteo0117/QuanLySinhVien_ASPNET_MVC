using QuanLySinhVien.Extensions;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLySinhVien.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_unitOfWork.UserRepository.IsValidUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, true);
                if(_unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name).RoleId == 1)
                {
                    return RedirectToAction("Index", "QuanLySinhVien");
                }    
                return RedirectToAction("Register", "ClassRegister");
            }   
            return View();
        }
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();           
            return RedirectToAction("Login");
        }
    }

}