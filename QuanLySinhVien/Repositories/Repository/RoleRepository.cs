using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext _dbContext;
        internal DbSet<Role> dbSet;
        public RoleRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.dbSet = _dbContext.Set<Role>();
        }
        public virtual void Add(Role entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(Role entity)
        {
            dbSet.Remove(entity);
        }

        public virtual IQueryable<Role> GetAll()
        {
            return dbSet;
        }

        public virtual Role GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(Role entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

    }
}