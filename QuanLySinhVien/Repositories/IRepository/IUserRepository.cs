using QuanLySinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.IRepository
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User GetById(int id);
        User GetByName(string username);
        bool IsValidUser(string username, string password);
        void UpdateUser(User user);
        void DeleteUser(int id);
        void AddUser(User user);
    }
}