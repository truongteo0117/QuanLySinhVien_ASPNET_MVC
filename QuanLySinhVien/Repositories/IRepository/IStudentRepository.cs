using QuanLySinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.IRepository
{
    public interface IStudentRepository
    {
        SinhVien GetById(int id);
        IQueryable<SinhVien> GetAll();
        void Add(SinhVien entity);
        void Update(SinhVien entity);
        void Delete(SinhVien entity);
        SinhVien IdMax(SinhVien entity);
    }
}