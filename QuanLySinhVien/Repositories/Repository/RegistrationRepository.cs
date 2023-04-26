using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly DbContext _dbContext;
        internal DbSet<DangKyLopHoc> dbSet;
        public RegistrationRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.dbSet = _dbContext.Set<DangKyLopHoc>();
        }
        public virtual void Add(DangKyLopHoc entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(DangKyLopHoc entity)
        {
            dbSet.Remove(entity);

        }
        public virtual void RemoveRange(IEnumerable<DangKyLopHoc> entity)
        {
            dbSet.RemoveRange(entity);
        }
        public virtual IQueryable<DangKyLopHoc> GetAll()
        {
            return dbSet;
        }

        public virtual DangKyLopHoc GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(DangKyLopHoc entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual int Total(int Id)
        {
            return dbSet.Where(t=>t.IdMonHoc == Id).Count();
        }
    }
}