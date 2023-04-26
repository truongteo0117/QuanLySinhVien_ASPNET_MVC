using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        internal DbContext _dbContext;
        internal DbSet<MonHoc> dbSet;
        public SubjectRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.dbSet = _dbContext.Set<MonHoc>();
        }
        public virtual void Add(MonHoc entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(MonHoc entity)
        {
            dbSet.Remove(entity);
        }

        public virtual IQueryable<MonHoc> GetAll()
        {
            return dbSet;
        }

        public virtual MonHoc GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(MonHoc entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}