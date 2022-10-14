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
                .AddSingleton<IRepo, Repo>()
                .BuildServiceProvider();

        static IRepo? repo = serviceProvider.GetService<IRepo>();


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

        [Fact]
        public void AddMultilpeProduct()
        {
            //ARRANGE
            for (int i = 0; i < 10; i++)
            {
                Product p = new()
                {
                    Name = $"Tier {i} sub",
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

        [Fact]
        public void UpdateProduct()
        {
            //ARRANGE
            Product p = repo.FindProductById(1);

            //ACT
            repo.UpdateEntit(p);

            //ASSERT
            Product product = repo.FindProductById(p.ProductId);
            Assert.Equal(p, product);
        }

        [Fact]
        public void UpdateMultiProduct()
        {
            //ARRANGE
            for (int i = 0; i < 10; i++)
            {
                Product p = repo.FindProductById(1);

                //ACT
                repo.UpdateEntit(p);

                //ASSERT
                Product product = repo.FindProductById(p.ProductId);
                Assert.Equal(p, product);
            }
        }
    }
}