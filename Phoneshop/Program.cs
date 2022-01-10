using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoneshop.Business;
using Phoneshop.Business.Data;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoneshop
{
    public class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var phoneService = serviceProvider.GetRequiredService<IPhoneService>();

            MainMenu(phoneService);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Phoneshop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true"), ServiceLifetime.Scoped);
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        }

        public static void MainMenu(IPhoneService phoneService)
        {
            List<Phone> listOfPhones = phoneService.GetAll().ToList();

            var index = 1;

            foreach (var phone in listOfPhones)
            {
                Console.WriteLine($"{index}. {phone.Brand}, {phone.Type}");
                index++;
            }
            Console.WriteLine($"{listOfPhones.Count + 1}. Search");

            Console.Write("\nType the number of the option you want: ");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out int number))
            {
                Console.Clear();
                Console.WriteLine("invalid number \n");

                MainMenu(phoneService);
            }

            if (number == (listOfPhones.Count + 1))
                Search(phoneService);
            else
            {
                int id = listOfPhones[number - 1].Id;
                Details(id, phoneService);
            }
        }

        private static void Details(int id, IPhoneService phoneService)
        {
            Console.Clear();

            Phone phoneFound = phoneService.Get(id);

            if (phoneFound == null)
            {
                Console.Clear();
                Console.WriteLine("phone not found \n");

                MainMenu(phoneService);
            }

            Console.WriteLine($"{phoneFound.Brand}, {phoneFound.Type}, {phoneFound.PriceWithTax} \n");
            Console.WriteLine($"{phoneFound.Description}");

            Console.ReadKey();
            Console.Clear();
            MainMenu(phoneService);
        }

        private static void Search(IPhoneService phoneService)
        {
            Console.Clear();

            Console.Write("Search: ");
            var input = Console.ReadLine();

            var result = phoneService.Search(input).ToList();

            foreach (var phone in result)
            {
                Console.WriteLine($"{phone.Brand}");
            }

            Console.WriteLine("\nPress a key to go back");
            Console.ReadKey();
            Console.Clear();
            MainMenu(phoneService);
        }
    }
}
