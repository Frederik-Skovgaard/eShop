﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class eShopContext : DbContext
    {
        public eShopContext(DbContextOptions<eShopContext> options)
            : base(options)
        {

        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<ProductUser>? productUsers { get; set; }
        public DbSet<UserInformation>? UserInformations { get; set; }
        public DbSet<PaymentMethodUser>? PaymentMethodUsers { get; set; }
        public DbSet<PaymentMethod>? PaymentMethods { get; set; }
        public DbSet<Types>? Types { get; set; }
        public DbSet<Role>? Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductUser>().HasKey(e => new { e.UserId, e.ProductId });
            modelBuilder.Entity<PaymentMethodUser>().HasKey(e => new { e.UserId, e.PaymentMethodId });

            modelBuilder.Entity<Product>().Property(p => p.Price)
               .HasColumnType("decimal(18,2)");

            #region User
            modelBuilder.Entity<UserInformation>().HasData(
                new UserInformation { UserInformationId = 1, Street = "Problembarngade 5", City = "Kolding", ZipCode = 6000 },
                 new UserInformation { UserInformationId = 2, Street = "Fyunkergade 2", City = "Sønderborg", ZipCode = 6400 },
                  new UserInformation { UserInformationId = 3, Street = "Sondsgade 4", City = "Aabenraa", ZipCode = 6300 });

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Admin" },
                new Role { RoleId = 2, Name = "SoftAdmin" },
                new Role { RoleId = 3, Name = "User" });

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "Admin", Password = "P@ssw0rd", Email ="gigga@gmail.com", UserInformationId = 1, RoleId = 1 },
                new User { UserId = 2, UserName = "Rene", Password = "kodeord", Email = "megaa@gmail.com", RoleId = 3 },
                new User { UserId = 3, UserName = "Benjamin", Password = "kodeord", Email = "behemoth@gmail.com", RoleId = 3 });

            #endregion

            #region  Prodcut & Cart
            modelBuilder.Entity<Types>().HasData(
                    new Types { TypesId = 1, Name = "Keyboards" },
                    new Types { TypesId = 2, Name = "GPU's" },
                    new Types { TypesId = 3, Name = "CPU's" },
                    new Types { TypesId = 4, Name = "Motherboard" });


            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "3080 RTX Nvida", Price = 10099.99M, Brand = "Nvida", ImageUrl = @"Images\EggFace.png", TypesId = 2 },
                new Product { ProductId = 2, Name = "3090 RTX Nvida", Price = 15999.99M, Brand = "Nvida", ImageUrl = @"Images\DinboyWithGlasses.png", TypesId = 2 },
                new Product { ProductId = 3, Name = "LogiTech Meistro Keyboard", Price = 1599.99M, Brand = "LogiTech", ImageUrl = @"Images\ducklingFace.png", TypesId = 1 },
                new Product { ProductId = 4, Name = "Asus Motherboard 3000x", Price = 2599.99M, Brand = "Asus", ImageUrl = @"Images\gundoggo.png", TypesId = 4 },
                new Product { ProductId = 5, Name = "AMD ThredRipper 9999x", Price = 59999.99M, Brand = "AMD", ImageUrl = @"Images\suitdoggo.png", TypesId = 3 });


            modelBuilder.Entity<ProductUser>().HasData(
                new ProductUser { UserId = 1, ProductId = 1, Quantity = 5 },
                new ProductUser { UserId = 3, ProductId = 1, Quantity = 5 });
            #endregion
        }
    }
}
