using QuanLySinhVien.Models;
using QuanLySinhVien.Repositories.IRepository;
using QuanLySinhVien.Repositories.Repository;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace QuanLySinhVien
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // register DbContext
            container.RegisterType<DbContext, DBSinhVienContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            // register IGenericRepository and GenericRepository
            //container.RegisterType(typeof(ICommonRepository<>), typeof(ICommonRepository<>));
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IClassRepository, ClassRepository>();
            container.RegisterType<INotificationRepository, NotificationRepository>();
            container.RegisterType<IRegistrationRepository, RegistrationRepository>();
            container.RegisterType<ISubjectRepository, SubjectRepository>();
            // register UnitOfWork

        }
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}