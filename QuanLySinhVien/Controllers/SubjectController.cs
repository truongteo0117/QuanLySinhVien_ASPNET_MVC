using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVien.Controllers
{
    public class SubjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Subject
        public ActionResult Index()
        {
            return View(_unitOfWork.SubjectRepository.GetAll());
        }

        // GET: Subject/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        public ActionResult Create(MonHoc model)
        {
            try
            {
                // TODO: Add insert logic here
                _unitOfWork.SubjectRepository.Add(model);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_unitOfWork.SubjectRepository.GetById(id));
        }

        // POST: Subject/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MonHoc model)
        {
            try
            {
                // TODO: Add update logic here
                _unitOfWork.SubjectRepository.Update(model);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                _unitOfWork.Rollback();
                return View();
            }
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int id)
        {
            _unitOfWork.SubjectRepository.Delete(_unitOfWork.SubjectRepository.GetById(id));
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        // POST: Subject/Delete/5
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
