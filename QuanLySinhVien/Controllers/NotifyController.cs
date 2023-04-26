using Antlr.Runtime.Misc;
using QuanLySinhVien.Extensions;
using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVien.Controllers
{
    [Authorize]
    public class NotifyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotifyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Notify
        public ActionResult Index()
        {
            DateTime date = DateTime.Now;
            var user = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name);
            if(user.RoleId == 1)
            {
                return View(_unitOfWork.NotificationRepository.GetAll());
            }    
            return View(_unitOfWork.NotificationRepository.GetAll().Where(x=>x.UserId == user.UserId));
        }

        // GET: Notify/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notify/Create
        public ActionResult Create(string action)
        {
            
            return View();
        }
        // POST: Notify/Create
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

        // GET: Notify/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notify/Edit/5
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

        // GET: Notify/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        public ActionResult RemoveNotify(int id)
        {
            try
            {
                var data = _unitOfWork.NotificationRepository.GetById(id);
                _unitOfWork.NotificationRepository.Delete(data);

                _unitOfWork.Commit();
                _unitOfWork.Dispose();
            }
            catch
            {
                _unitOfWork.Rollback();
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveAllNotify()
        {
            try
            {
                var data = _unitOfWork.NotificationRepository.GetAll();
                _unitOfWork.NotificationRepository.RemoveRange(data);

                _unitOfWork.Commit();
                _unitOfWork.Dispose();
            }
            catch 
            {
                _unitOfWork.Rollback();
            }
            return RedirectToAction("Index");
        }
        // POST: Notify/Delete/5
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
