using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Phoneshop.Business.Data
{
    [ExcludeFromCodeCoverage]
    public class DataContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Phone>().Navigation(x => x.Brand).AutoInclude();
            modelBuilder.Entity<Order>().Navigation(x => x.ProductsPerOrder).AutoInclude();
            modelBuilder.Entity<ProductOrder>().Navigation(x => x.Phone).AutoInclude();

            modelBuilder.Entity<ProductOrder>().HasKey(p => new {p.OrderId, p.PhoneId});


            modelBuilder.Entity<Brand>().HasData(
            new Brand
            {
                Id = 1,
                Name = "Huawei"
            },
            new Brand
            {
                Id = 2,
                Name = "Samsung"
            },
            new Brand
            {
                Id = 3,
                Name = "Apple"
            },
            new Brand
            {
                Id = 4,
                Name = "Google"
            },
            new Brand
            {
                Id = 5,
                Name = "Xiaomi"
            });

            modelBuilder.Entity<Phone>().HasData(
            new Phone
            {
                Id = 1,
                BrandID = 1,
                Type = "P30",
                PriceWithTax = 697,
                Stock = 34,
                Description = "6.47\" FHD+ (2340x1080) OLED, \nKirin 980 Octa - Core(2x Cortex - A76 2.6GHz + 2x Cortex - A76 1.92GHz + 4x Cortex - A55 1.8GHz), \n8GB RAM, 128GB ROM, 40 + 20 + 8 + TOF / 32MP, \nDual SIM, 4200mAh, Android 9.0 + EMUI 9.1"
            },
            new Phone
            {
                Id = 2,
                BrandID = 2,
                Type = "Galaxy A52",
                PriceWithTax = 399,
                Stock = 23,
                Description = "64 megapixel camera, 4k videokwaliteit \n6.5 inch AMOLED scherm \n128 GB opslaggeheugen (Uitbreidbaar met Micro-sd) \nWater- en stofbestendig (IP67)"
            },
            new Phone
            {
                Id = 3,
                BrandID = 3,
                Type = "IPhone 11",
                PriceWithTax = 619,
                Stock = 39,
                Description = "Met de dubbele camera schiet je in elke situatie een perfecte foto of video \nDe krachtige A13-chipset zorgt voor razendsnelle prestaties \nMet Face ID hoef je enkel en alleen naar je toestel te kijken om te ontgrendelen \nHet toestel heeft een lange accuduur dankzij een energiezuinige processor"
            },
            new Phone
            {
                Id = 4,
                BrandID = 4,
                Type = "Pixel 4a",
                PriceWithTax = 411,
                Stock = 21,
                Description = "12.2 megapixel camera, 4k videokwaliteit \n5.81 inch OLED scherm \n128 GB opslaggeheugen \n3140 mAh accucapaciteit"
            },
            new Phone
            {
                Id = 5,
                BrandID = 5,
                Type = "Redmi Note 10 Pro",
                PriceWithTax = 298,
                Stock = 98,
                Description = "108 megapixel camera, 4k videokwaliteit \n6.67 inch AMOLED scherm \n128 GB opslaggeheugen (Uitbreidbaar met Micro-sd) \nWater- en stofbestendig (IP53)"
            });
        }
    }
}
