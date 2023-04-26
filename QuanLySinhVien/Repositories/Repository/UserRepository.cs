using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;
        internal DbSet<User> dbSet;
        public UserRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.dbSet = _dbContext.Set<User>();
        }
        public virtual void AddUser(User entity)
        {
            dbSet.Add(entity);
        }

        public virtual void DeleteUser(int id)
        {
            var d = dbSet.Where(x => x.UserId == id).FirstOrDefault();
            dbSet.Remove(d);
        }
        public virtual IQueryable<User> GetAll()
        {
            return dbSet;
        }

        public virtual User GetById(int id)
        {
            return dbSet.Find(id);
        }
        public virtual User GetByName(string name)
        {
            return dbSet.Where(x => x.UserName == name).FirstOrDefault();
        }
        public bool IsValidUser(string username, string password)
        {
            bool isValid = false;
            var user = dbSet.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                isValid = true;
            }
            return isValid;
        }

        public virtual void UpdateUser(User entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}