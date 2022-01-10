using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoneshop.Business;
using Phoneshop.Business.Data;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System.Collections.Generic;

namespace ImportTool.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var importService = serviceProvider.GetRequiredService<IImportService>();
            var phoneService = serviceProvider.GetRequiredService<IPhoneService>();


            List<Phone> importList = importService.ConvertXmlToList(args[0]);

            foreach (var item in importList)
            {
                phoneService.Create(item);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Phoneshop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true"), ServiceLifetime.Scoped);
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IImportService, ImportService>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        }
    }
}
