using QuanLySinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.IRepository
{
    public interface INotificationRepository
    {
        ThongBao GetById(int id);
        IQueryable<ThongBao> GetAll();
        void Add(ThongBao entity);
        void Update(ThongBao entity);
        void Delete(ThongBao entity);
        void RemoveRange(IEnumerable<ThongBao> entity);
        void Add(int id, string studentname, string action);
        void Add(int id, string studentname, string action, string className);
    }
}