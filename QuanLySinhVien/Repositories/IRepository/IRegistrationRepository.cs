using QuanLySinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.IRepository
{
    public interface IRegistrationRepository
    {
        DangKyLopHoc GetById(int id);
        IQueryable<DangKyLopHoc> GetAll();
        void Add(DangKyLopHoc entity);
        void Update(DangKyLopHoc entity);
        void Delete(DangKyLopHoc entity);
        int Total(int id);
        void RemoveRange(IEnumerable<DangKyLopHoc> entity);
    }
}