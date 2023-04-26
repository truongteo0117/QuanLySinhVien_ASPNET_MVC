using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DbContext _dbContext;
        internal DbSet<ThongBao> dbSet;
        public NotificationRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.dbSet = _dbContext.Set<ThongBao>();
        }
        public virtual void Add(ThongBao entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(ThongBao entity)
        {
            dbSet.Remove(entity);
        }
        public virtual  void RemoveRange(IEnumerable<ThongBao> entity)
        {
            dbSet.RemoveRange(entity);
        }    
        public virtual IQueryable<ThongBao> GetAll()
        {
            return dbSet;
        }

        public virtual ThongBao GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(ThongBao entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Add(int id, string studentname, string action)
        {
            ThongBao thongBao = new ThongBao();
            thongBao.UserId = id;
            thongBao.NoiDung = $"[${thongBao.NgayTao}] User [{studentname}] has been [{action}]";

            dbSet.Add(thongBao);
        }
        public virtual void Add(int id, string studentname, string action, string subjectName)
        {
            ThongBao thongBao = new ThongBao();
            thongBao.UserId = id;
            thongBao.NoiDung = $"[{thongBao.NgayTao}] User [{studentname}] has [{action}] for [{subjectName}]";

            dbSet.Add(thongBao);
        }
        
    }
}