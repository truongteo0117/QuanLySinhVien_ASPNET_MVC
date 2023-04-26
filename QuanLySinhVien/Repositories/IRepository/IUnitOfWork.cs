using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Repositories.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IClassRepository ClassRepository { get; }
        IStudentRepository StudentRepository { get; }
        IRoleRepository RoleRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IRegistrationRepository RegistrationRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        void Commit();
        void Rollback();
    }
}