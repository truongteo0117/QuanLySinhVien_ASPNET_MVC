using Antlr.Runtime.Tree;
using Microsoft.Ajax.Utilities;
using QuanLySinhVien.Extensions;
using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Windows.Media.Media3D;

namespace QuanLySinhVien.Controllers
{
    [Authorize]
    public class ClassRegisterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassRegisterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }

        // GET: ClassRegister
        public ActionResult Index()
        {
            return View(_unitOfWork.RegistrationRepository.GetAll());
        }

        // GET: ClassRegister/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult ViewRegister()
        {
            // TODO: Add insert logic here
            var id = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name).SinhVien.StudentId;
            if(!CheckRole())
            {
                return PartialView("View", _unitOfWork.RegistrationRepository.GetAll());
            }    
            var data= _unitOfWork.RegistrationRepository.GetAll().Where(r=>r.StudentId==id).Where(r=>r.IdMonHoc!=null);
            ViewBag.Total = TotalTinChi(id);
            return PartialView("View", data);
        }
        // GET: ClassRegister/Create
        public ActionResult Register()
        {
            var ddMonHoc = _unitOfWork.SubjectRepository.GetAll();
            foreach(var item in ddMonHoc)
            {
                item.TenMonHoc = $"{item.TenMonHoc} - ({item.TinChiMon}) Tín chỉ";
            }    
            ViewBag.IdMonHoc = new SelectList(ddMonHoc, "MonHocId", "TenMonHoc");
            ViewBag.isShow = CheckRole();
            return View();
        }

        // POST: ClassRegister/Create
        [HttpPost]
        public ActionResult Register(DangKyLopHoc model)
        {
            try
            {
                var sv = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name).SinhVien;
                model.StudentId = sv.StudentId;
                if (CheckTotal((int)model.IdMonHoc) && CheckSubject((int)model.IdMonHoc,sv.StudentId))
                {
                    model.LopId = sv.LopId;
                    _unitOfWork.RegistrationRepository.Add(model);
                    var user = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name);
                    _unitOfWork.NotificationRepository.Add(user.UserId, user.SinhVien.HoTen, "Registed", _unitOfWork.SubjectRepository.GetById((int)model.IdMonHoc).TenMonHoc);
                    _unitOfWork.Commit();
                    return RedirectToAction("Register");
                }
                return RedirectToAction("Register");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClassRegister/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClassRegister/Edit/5
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

        // GET: ClassRegister/Delete/5
        public ActionResult Delete(int id)
        {
            var data = _unitOfWork.RegistrationRepository.GetById(id);
            _unitOfWork.RegistrationRepository.Delete(data);
            var user = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name);
            
            _unitOfWork.Commit();
            _unitOfWork.Dispose();
            return RedirectToAction("Register");
        }

        // POST: ClassRegister/Delete/5
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
        #region Extend Function
        public void AddNotify(int userId, string hoTen, string action, string tenMonHoc)
        {
            _unitOfWork.NotificationRepository.Add(userId, hoTen, action, tenMonHoc);
        }
        public bool CheckSubject(int monHocID, int studentId)
        {
            var data = _unitOfWork.RegistrationRepository.GetAll()
                .Where(d=>d.IdMonHoc == monHocID)
                .Where(d => d.StudentId == studentId)
                .FirstOrDefault();
            if(data!=null)
            {
                return false;
            }    
            return true;
        }
        public bool CheckTotal(int monHocId)
        {
            var total = _unitOfWork.RegistrationRepository.Total(monHocId);
            var configTotal = int.Parse(ConfigurationManager.AppSettings["TotalLimit"]);
            if(total >= configTotal)
            {
                return false;
            }    
            return true;
        }
        public int TotalTinChi(int id)
        {
            var count = 0;
            var data = _unitOfWork.RegistrationRepository.GetAll().Where(x => x.StudentId == id).Where(x=>x.IdMonHoc !=null);
            if(data != null)
            {
                foreach (var item in data)
                {
                    count += item.MonHoc.TinChiMon;
                }
            }     
            return count;
        }
        public bool CheckRole()
        {
            var id = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name).RoleId;
            switch(id)
            {
                case 1: return false;
            }
            return true;
        }
        #endregion  
    }
}
