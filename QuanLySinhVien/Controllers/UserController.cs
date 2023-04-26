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
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private int uStudentId { get; set;}
        private int uRoleId { get; set;}
        public UserController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        // GET: User
        public ActionResult Index()
        {
            //var formsAuthCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (formsAuthCookie != null)
            //{
            //    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(formsAuthCookie.Value);
            //    uStudentId = _unitOfWork.UserRepository.GetByName(authTicket.Name).UserId;
            //    uRoleId = _unitOfWork.UserRepository.GetByName(authTicket.Name).RoleId;
            //}
            return View(_unitOfWork.UserRepository.GetAll());
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            var ddRole = _unitOfWork.RoleRepository.GetAll();
            var ddLop = _unitOfWork.ClassRepository.GetAll();
            ViewBag.LopId = new SelectList(ddLop, "LopId", "TenLop");
            ViewBag.RoleId = new SelectList(ddRole, "RoleId", "RoleName");
            return View();
        }
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User model)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                model.SinhVien.StudentId = _unitOfWork.StudentRepository.IdMax(model.SinhVien).StudentId;
                _unitOfWork.UserRepository.AddUser(model);

                _unitOfWork.StudentRepository.Add(model.SinhVien);

                _unitOfWork.Commit();
                _unitOfWork.Dispose();
                // TODO: Add insert logic here
            }
            catch
            {
                _unitOfWork.Rollback();
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult ResetPassword(int id)
        {
            return View();
        }
        public ActionResult Permission(int id)
        { return View(); }
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Index");
        }
        
        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            
            var user = _unitOfWork.UserRepository.GetById(id);
            int studentId = int.Parse(user.StudentId.ToString());
            var thongBao = _unitOfWork.NotificationRepository.GetAll().Where(x => x.UserId == user.UserId);
            _unitOfWork.NotificationRepository.RemoveRange(thongBao);
            _unitOfWork.UserRepository.DeleteUser(id);
            _unitOfWork.StudentRepository.Delete(_unitOfWork.StudentRepository.GetById(studentId));
            _unitOfWork.RegistrationRepository.RemoveRange(_unitOfWork.RegistrationRepository.GetAll().Where(r=>r.StudentId == studentId));
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        // POST: User/Delete/5
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
        private void AddNotifyData(int id, string action)
        {
            ThongBao thongBao = new ThongBao();
            thongBao.UserId = id;
            thongBao.NoiDung = action;

            _unitOfWork.NotificationRepository.Add(thongBao);

        }
    }
}
