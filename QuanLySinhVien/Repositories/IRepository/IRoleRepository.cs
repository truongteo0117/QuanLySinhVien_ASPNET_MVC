using QuanLySinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.IRepository
{
    public interface IRoleRepository
    {
        Role GetById(int id);
        IQueryable<Role> GetAll();
        void Add(Role entity);
        void Update(Role entity);
        void Delete(Role entity);
    }
}