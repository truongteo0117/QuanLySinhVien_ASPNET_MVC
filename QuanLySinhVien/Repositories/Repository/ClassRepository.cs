using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.Repository
{
    public class ClassRepository : ICommonRepository<LopHoc>, IClassRepository
    {
        private readonly DbContext _dbContext;
        internal DbSet<LopHoc> dbSet;
        public ClassRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.dbSet = _dbContext.Set<LopHoc>();
        }
        public virtual void Add(LopHoc entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(LopHoc entity)
        {
            dbSet.Remove(entity);
        }

        public virtual IQueryable<LopHoc> GetAll()
        {
            return dbSet;
        }

        public virtual LopHoc GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(LopHoc entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}