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
    public class ClassController : Controller
    {
        // GET: Class
        private readonly IUnitOfWork _unitOfWork;
        public ClassController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View(_unitOfWork.ClassRepository.GetAll());
        }

        // GET: Class/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Class/Create
        [HttpPost]
        public ActionResult Create(LopHoc model)
        {
            try
            {
                // TODO: Add insert logic here
                _unitOfWork.ClassRepository.Add(model);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                _unitOfWork.Rollback();
                return View();
            }
        }

        // GET: Class/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_unitOfWork.ClassRepository.GetById(id));
        }

        // POST: Class/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LopHoc model)
        {
            try
            {
                // TODO: Add update logic here
                _unitOfWork.ClassRepository.Update(model);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                _unitOfWork.Rollback();
                return View();
            }
        }

        // GET: Class/Delete/5
        public ActionResult Delete(int id)
        {
            _unitOfWork.ClassRepository.Delete(_unitOfWork.ClassRepository.GetById(id));
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        // POST: Class/Delete/5
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
