using AdoForm.BLL.Servises;
using AdoForm.BLL.Utils;
using AdoForm.DAL;
using AdoForm.DAL.Entities;
using AdoForm.DAL.Repository;
using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoForm3LayerClient
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
                builder.RegisterType<AdoDALContext>().As<DbContext>().SingleInstance();
                builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IGenericRepository<>));
                builder.RegisterType<AdoFormServises>().As<IAdoFormServises>();
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
