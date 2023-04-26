using OfficeOpenXml;
using QuanLySinhVien.Extensions;
using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows.Media;
using WebGrease.Activities;

namespace QuanLySinhVien.Controllers
{
    [Authorize]
    public class QuanLySinhVienController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        internal int IdUser { get; set; }
        public QuanLySinhVienController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: QuanLySinhVien

        public ActionResult Index()
        {
            //ExportToExcel();
            var listData = _unitOfWork.StudentRepository.GetAll();
            ViewBag.isShowEP = CheckRole()?null:"isUser";
            return View(listData.ToList());
        }

        // GET: QuanLySinhVien/Details/5
        public ActionResult Details(int id)
        {
            var data = _unitOfWork.StudentRepository.GetById(id);
            return View(data);
        }

        // GET: QuanLySinhVien/Create
        public ActionResult Create()
        {
            var dropdownDT = _unitOfWork.ClassRepository.GetAll();
            ViewBag.LopId = new SelectList(dropdownDT, "LopId", "TenLop");
            return View();
        }

        // POST: QuanLySinhVien/Create: SinhVien
        [HttpPost]
        public ActionResult Create(SinhVien model)
        {
            try
            {
                // TODO: Add insert logic here
                var modelNew = _unitOfWork.StudentRepository.IdMax(model);
                _unitOfWork.StudentRepository.Add(modelNew);

                var user = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name);
                _unitOfWork.NotificationRepository.Add(user.UserId, user.SinhVien.HoTen, "Created");

                _unitOfWork.Commit();
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                _unitOfWork.Rollback();
                return View(model);
            }
        }

        // GET: QuanLySinhVien/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _unitOfWork.ClassRepository.GetAll();
            var currentData = _unitOfWork.StudentRepository.GetById(id);
            ViewBag.LopId = new SelectList(data, "LopId", "TenLop", currentData.LopId);

            return View(currentData);
        }

        // POST: QuanLySinhVien/Edit/5
        [HttpPost]
        public ActionResult Edit(SinhVien model)
        {
            try
            {
                _unitOfWork.StudentRepository.Update(model);
                var user = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name);
                _unitOfWork.NotificationRepository.Add(user.UserId, user.SinhVien.HoTen, "Updated");

                _unitOfWork.Commit();
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                _unitOfWork.Rollback();
                return View();
            }
        }

        // GET: QuanLySinhVien/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_unitOfWork.StudentRepository.GetById(id));
        }

        // POST: QuanLySinhVien/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var data = _unitOfWork.StudentRepository.GetById(id);
                _unitOfWork.StudentRepository.Delete(data);
                var user = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name);
                _unitOfWork.NotificationRepository.Add(user.UserId, user.SinhVien.HoTen, "Deleted");

                _unitOfWork.Commit();
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                _unitOfWork.Rollback();
                return View();
            }
        }
        #region Extended Function
        public ActionResult ExportToExcel()
        {
            DataTable dt = new DataTable();

            BuildCollums(dt);
            BuildRow(dt);

            // Create Excel package

            ExcelPackage package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Thông Tin Sinh Viên");
            ExcelWorksheet worksheetRg = package.Workbook.Worksheets.Add("Đăng Ký Môn Học");
            ExportRegisterDataToExcel(worksheetRg);
            worksheet.Cells["A1"].LoadFromDataTable(dt, true);

            worksheet.Columns.AutoFit();

            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            string fileName = $"SinhVienData_{DateTime.Now.ToString("ddMMyyyHHmmss")}.xlsx";

            byte[] excelBytes = package.GetAsByteArray();

            // Set content type and headers for response
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(excelBytes);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush();
            return RedirectToAction("Index");
        }
        private void BuildCollums(DataTable dt)
        {
            dt.Columns.Add("STT");
            dt.Columns.Add("Mã Sinh Viên");
            dt.Columns.Add("Họ Và Tên");
            dt.Columns.Add("Giới Tính");
            dt.Columns.Add("Ngày Sinh");
            dt.Columns.Add("Tên Lớp");
            dt.Columns.Add("UserName");
            dt.Columns.Add("Password");
            dt.Columns.Add("Quyền");
            dt.Columns.Add("Trạng Thái");
        }
        private void BuildRow(DataTable dt)
        {
            var data = _unitOfWork.StudentRepository.GetAll();

            var count = 1;
            foreach (var item in data)
            {
                dt.Rows.Add(count++,
                    item.MaSV,
                    item.HoTen,
                    item.GioiTinh ? "Nam" : "Nữ",
                    item.NgaySinh.ToString("dd-MM-yyyy"),
                    item.LopHoc.TenLop,
                    item.Users.FirstOrDefault().UserName,
                    item.Users.FirstOrDefault().Password,
                    item.Users.FirstOrDefault().Role.RoleName,
                    item.IsActive ? "Đang Hoạt Động" : "Ngưng Hoạt Động");
            }
        }
        private void ExportRegisterDataToExcel(ExcelWorksheet worksheet)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("STT");
            dataTable.Columns.Add("Mã Sinh Viên");
            dataTable.Columns.Add("Họ Và Tên");
            dataTable.Columns.Add("Tên Lớp");
            dataTable.Columns.Add("Môn Đăng Ký");
            dataTable.Columns.Add("Số Tín Chỉ");

            BuildRowForRegister(dataTable);

            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
            worksheet.Columns.AutoFit();

            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        }
        private void BuildRowForRegister(DataTable dt)
        {
            var data = _unitOfWork.RegistrationRepository.GetAll();

            var count = 1;
            foreach (var item in data)
            {
                dt.Rows.Add(count++,
                    item.SinhVien.MaSV,
                    item.SinhVien.HoTen,
                    item.LopHoc.TenLop,
                    item.MonHoc.TenMonHoc,
                    item.MonHoc.TinChiMon);
            }
        }
        public bool CheckRole()
        {
            var id = _unitOfWork.UserRepository.GetByName(Request.GetAuthTicket().Name).RoleId;
            switch (id)
            {
                case 1: return false;
            }
            return true;
        }
        #endregion
    }
}
