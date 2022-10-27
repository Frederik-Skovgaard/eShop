using System;
using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Interface;
using ServiceLayer.Repository;

namespace UnitTest
{
    public class UnitTest1
    {
        static ServiceProvider? serviceProvider = new ServiceCollection()
                .AddSingleton<IProduct, ProductService>()
                .BuildServiceProvider();

        static IProduct? repo = serviceProvider.GetService<IProduct>();


        [Fact]
        public void AddProduct()
        {
            //ARRANGE
            Product p = new()
            {
                Name = "Tier 3 sub",
                Brand = "Twitch",
                Price = 20.99M,
                TypesId = 1
            };

            //ACT
            repo.AddEntity(p);


            //ASSERT
            Product product = repo.FindProductById(p.ProductId);
            Assert.Equal(p, product);
        }
    }
}