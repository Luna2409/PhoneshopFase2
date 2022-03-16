using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoneshop.Business.Data;
using Phoneshop.Business.Repositories;
using Phoneshop.Business.Services;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var phoneService = serviceProvider.GetRequiredService<IPhoneService>();

            await MainMenu(phoneService);
        }

        public static async Task MainMenu(IPhoneService phoneService)
        {
            List<Phone> listOfPhones = phoneService.GetAll().ToList();

            var index = 1;

            foreach (var phone in listOfPhones)
            {
                Console.WriteLine($"{index}. {phone.Brand.Name}, {phone.Type}");
                index++;
            }
            Console.WriteLine($"{listOfPhones.Count + 1}. Search");

            Console.Write("\nType the number of the option you want: ");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out int number))
            {
                Console.Clear();
                Console.WriteLine("invalid number \n");

                await MainMenu(phoneService);
            }

            if (number == (listOfPhones.Count + 1))
                await Search(phoneService);
            else
            {
                int id = listOfPhones[number - 1].Id;
                await Details(id, phoneService);
            }
        }

        private static async Task Details(int id, IPhoneService phoneService)
        {
            Console.Clear();

            Phone phoneFound = await phoneService.GetByIdAsync(id);

            if (phoneFound == null)
            {
                Console.Clear();
                Console.WriteLine("phone not found \n");

                await MainMenu(phoneService);
            }

            Console.WriteLine($"{phoneFound.Brand.Name}, {phoneFound.Type}, {phoneFound.PriceWithTax} \n");
            Console.WriteLine($"{phoneFound.Description}");

            Console.ReadKey();
            Console.Clear();
            await MainMenu(phoneService);
        }

        private static async Task Search(IPhoneService phoneService)
        {
            Console.Clear();

            Console.Write("Search: ");
            var input = Console.ReadLine();

            var result = phoneService.Search(input).ToList();

            foreach (var phone in result)
            {
                Console.WriteLine($"{phone.Brand.Name}");
            }

            Console.WriteLine("\nPress a key to go back");
            Console.ReadKey();
            Console.Clear();
            await MainMenu(phoneService);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Phoneshop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true"), ServiceLifetime.Scoped);
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        }
    }
}
