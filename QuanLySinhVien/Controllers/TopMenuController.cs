using QuanLySinhVien.Extensions;
using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLySinhVien.Controllers
{
    public class TopMenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TopMenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: TopMenu
        public ActionResult Index()
        {
            try 
            {            
                var authTicket = Request.GetAuthTicket();
                if (authTicket.Name != string.Empty)
                {
                    var userInfo = _unitOfWork.UserRepository.GetByName(authTicket.Name);
                    var studentId = userInfo.StudentId;
                    var role = userInfo.RoleId;
                    ViewBag.UserName = authTicket.Name;
                    if (role == 1)
                    {
                        return PartialView("_Menu", GetMenuItemsForAdmin());
                    }
                    return PartialView("_Menu", GetMenuItemsForUser((int)studentId));
                }
            }
            catch{}
            
            return PartialView("_Menu", new List<MenuItem>());
        }
        private List<MenuItem> GetMenuItemsForUser(int studentId)
        {
            var classRegisterC = Url.Action("Register", "ClassRegister");
            var notifyC = Url.Action("Index", "Notify");
            var sinhVienC = Url.Action("Details", "QuanLySinhVien", new { id = studentId });
            var menu = new List<MenuItem>
            {
                new MenuItem { Id = 1, Name = "Thông Tin Cá Nhân", Url = sinhVienC },
                new MenuItem { Id = 2, Name = "Đăng Ký Lớp Học", Url = classRegisterC },
                new MenuItem { Id = 3, Name = "Thông Báo", Url = notifyC },
            };
            return menu;
        }
        private List<MenuItem> GetMenuItemsForAdmin()
        {
            var classC = Url.Action("Index", "Class");
            var monhocC = Url.Action("Index", "Subject");
            var notifyC = Url.Action("Index", "Notify");
            var userC = Url.Action("Index", "User");
            var sinhVienC = Url.Action("Index", "QuanLySinhVien");
            var classRegisterC = Url.Action("Register", "ClassRegister");
            var menu = new List<MenuItem>
            {
                new MenuItem { Id = 1, Name = "Sinh Viên", Url = sinhVienC },
                new MenuItem { Id = 2, Name = "Lớp Học", Url = classC },
                new MenuItem { Id = 3, Name = "Thông Báo", Url = notifyC },
                new MenuItem { Id = 4, Name = "Người Dùng", Url = userC },
                new MenuItem { Id = 5, Name = "Môn Học", Url = monhocC },
                new MenuItem { Id = 6, Name = "Đăng Ký Lớp Học", Url = classRegisterC },
            };
            return menu;
        }
        // GET: TopMenu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TopMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TopMenu/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TopMenu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TopMenu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TopMenu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TopMenu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
