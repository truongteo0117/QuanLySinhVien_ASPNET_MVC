using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DbContext _dbContext;
        internal DbSet<SinhVien> dbSet;
        public StudentRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.dbSet = _dbContext.Set<SinhVien>();
        }
        public virtual void Add(SinhVien entity)
        {
            dbSet.Add(entity);
        }
        public virtual SinhVien IdMax(SinhVien entity)
        {
            entity.StudentId = dbSet.Max(x => x.StudentId) + 1;
            return entity;
        }
        public virtual void Delete(SinhVien entity)
        {
            dbSet.Remove(entity);
        }

        public virtual IQueryable<SinhVien> GetAll()
        {
            return dbSet;
        }

        public virtual SinhVien GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(SinhVien entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}