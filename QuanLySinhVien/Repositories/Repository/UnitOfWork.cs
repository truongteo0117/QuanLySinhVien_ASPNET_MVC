using QuanLySinhVien.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static Unity.Storage.RegistrationSet;

namespace QuanLySinhVien.Repositories.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(_dbContext);
            StudentRepository = new StudentRepository(_dbContext);
            ClassRepository = new ClassRepository(_dbContext);
            RoleRepository = new RoleRepository(_dbContext);
            NotificationRepository = new NotificationRepository(_dbContext);
            RegistrationRepository = new RegistrationRepository(_dbContext);
            SubjectRepository = new SubjectRepository(_dbContext);
        }
        public IUserRepository UserRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public IClassRepository ClassRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public INotificationRepository NotificationRepository { get; private set; }
        public IRegistrationRepository RegistrationRepository { get; private set; }
        public ISubjectRepository SubjectRepository { get; private set; }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .Where(e => e.State != EntityState.Added)
                .ToList()
                .ForEach(e => e.Reload());
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}