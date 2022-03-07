﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Phoneshop.Business.Data;

namespace Phoneshop.Business.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Phoneshop.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Huawei"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Samsung"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Apple"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Google"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Xiaomi"
                        });
                });

            modelBuilder.Entity("Phoneshop.Domain.Entities.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceWithTax")
                        .HasColumnType("float");

                    b.Property<double>("PriceWithoutTax")
                        .HasColumnType("float");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandID");

                    b.ToTable("Phones");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandID = 1,
                            Description = "6.47\" FHD+ (2340x1080) OLED, \nKirin 980 Octa - Core(2x Cortex - A76 2.6GHz + 2x Cortex - A76 1.92GHz + 4x Cortex - A55 1.8GHz), \n8GB RAM, 128GB ROM, 40 + 20 + 8 + TOF / 32MP, \nDual SIM, 4200mAh, Android 9.0 + EMUI 9.1",
                            PriceWithTax = 697.0,
                            PriceWithoutTax = 0.0,
                            Stock = 34,
                            Type = "P30"
                        },
                        new
                        {
                            Id = 2,
                            BrandID = 2,
                            Description = "64 megapixel camera, 4k videokwaliteit \n6.5 inch AMOLED scherm \n128 GB opslaggeheugen (Uitbreidbaar met Micro-sd) \nWater- en stofbestendig (IP67)",
                            PriceWithTax = 399.0,
                            PriceWithoutTax = 0.0,
                            Stock = 23,
                            Type = "Galaxy A52"
                        },
                        new
                        {
                            Id = 3,
                            BrandID = 3,
                            Description = "Met de dubbele camera schiet je in elke situatie een perfecte foto of video \nDe krachtige A13-chipset zorgt voor razendsnelle prestaties \nMet Face ID hoef je enkel en alleen naar je toestel te kijken om te ontgrendelen \nHet toestel heeft een lange accuduur dankzij een energiezuinige processor",
                            PriceWithTax = 619.0,
                            PriceWithoutTax = 0.0,
                            Stock = 39,
                            Type = "IPhone 11"
                        },
                        new
                        {
                            Id = 4,
                            BrandID = 4,
                            Description = "12.2 megapixel camera, 4k videokwaliteit \n5.81 inch OLED scherm \n128 GB opslaggeheugen \n3140 mAh accucapaciteit",
                            PriceWithTax = 411.0,
                            PriceWithoutTax = 0.0,
                            Stock = 21,
                            Type = "Pixel 4a"
                        },
                        new
                        {
                            Id = 5,
                            BrandID = 5,
                            Description = "108 megapixel camera, 4k videokwaliteit \n6.67 inch AMOLED scherm \n128 GB opslaggeheugen (Uitbreidbaar met Micro-sd) \nWater- en stofbestendig (IP53)",
                            PriceWithTax = 298.0,
                            PriceWithoutTax = 0.0,
                            Stock = 98,
                            Type = "Redmi Note 10 Pro"
                        });
                });

            modelBuilder.Entity("Phoneshop.Domain.Entities.Phone", b =>
                {
                    b.HasOne("Phoneshop.Domain.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });
#pragma warning restore 612, 618
        }
    }
}
