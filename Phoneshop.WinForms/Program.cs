using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoneshop.Business;
using Phoneshop.Business.Data;
using Phoneshop.Domain.Interfaces;
using System;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var phoneOverview = serviceProvider.GetRequiredService<PhoneOverview>();
                Application.Run(phoneOverview);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Phoneshop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true"), ServiceLifetime.Scoped);
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<PhoneOverview>();
            services.AddScoped<AddPhone>();
        }
    }
}
