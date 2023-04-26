using QuanLySinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.IRepository
{
    public interface ISubjectRepository
    {
        MonHoc GetById(int id);
        IQueryable<MonHoc> GetAll();
        void Add(MonHoc entity);
        void Update(MonHoc entity);
        void Delete(MonHoc entity);
    }
}