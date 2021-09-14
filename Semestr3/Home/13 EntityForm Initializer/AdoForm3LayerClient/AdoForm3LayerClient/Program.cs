using Autofac;
using AutoMapper;
using EntityForm.BLL.Servises;
using EntityForm.BLL.Utils;
using EntityForm.DAL;
using EntityForm.DAL.Repository;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace EntityForm3LayerClient
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var builder = new ContainerBuilder();
                builder.RegisterType<EntityDALContext>().As<DbContext>().SingleInstance();
                builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IGenericRepository<>));
                builder.RegisterType<EntityFormServises>().As<IEntityFormServises>();
                builder.RegisterType<Form1>().AsSelf();

                var config = new MapperConfiguration(cgf => cgf.AddProfile(new MapperConfig()));
                builder.RegisterInstance(config.CreateMapper());

                using (var scope = builder.Build().BeginLifetimeScope())
                {
                    var window = scope.Resolve<Form1>();
                    window.ShowDialog();
                }
            } 
            catch(Exception ex)
            {
                MessageBox.Show("Проблема з сервером, або спровокований краш( \n" + ex.Message);
            }
        }

    }
}
