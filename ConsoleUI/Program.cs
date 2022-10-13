using Microsoft.EntityFrameworkCore;
using DataLayer;
using System;
using ServiceLayer.Repository;
using ServiceLayer.Interface;
using DataLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleUI
{
    class Program
    {
        static ServiceProvider? serviceProvider = new ServiceCollection()
                .AddSingleton<IRepo, Repo>()
                .BuildServiceProvider();

        static IRepo? repo = serviceProvider.GetService<IRepo>();


        static void Main(string[] args)
        {
            bool isTrue = true;

            while (isTrue)
            {
                Menu();
                var read = Console.ReadKey();
                Console.Clear();

                switch (read.Key)
                {
                    default:
                        break;
                    case ConsoleKey.D1:
                        CreateProduct();
                        break;
                    case ConsoleKey.D2:
                        DeleteProduct();
                        break;
                    case ConsoleKey.D3:
                        UpdateProduct();
                        break;
                    case ConsoleKey.D4:
                        GetProducts();
                        break;
                    case ConsoleKey.D5:
                        GetProductsByBrand();
                        break;
                    case ConsoleKey.D6:

                        break;
                    case ConsoleKey.D7:
                        isTrue = false;
                        break;
                }
            }

        }

        static void Menu()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("\t\t|**eShop**|");
            Console.WriteLine("1) Create product");
            Console.WriteLine("2) Delete product");
            Console.WriteLine("3) Update product");
            Console.WriteLine("4) Get all products");
            Console.WriteLine("5) Get prodcuts by brand name");
            Console.WriteLine("6) Get all users with specific product in there cart");
            Console.WriteLine("7) Exit");
            Console.WriteLine("------------------------------------------------------------");
        }

        static void CreateProduct()
        {
            Console.WriteLine("\t\t**Create Product**");
            Console.Write("Productnavn: ");
            string name = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Brand: ");
            string brandName = Console.ReadLine();

            Console.WriteLine("\n");

            foreach (var item in repo.GetTypes())
            {
                Console.WriteLine($"TypesId: {item.TypesId}) Type: {item.Name}");
            }

            Console.Write("Select a type: ");
            int type = Convert.ToInt32(Console.ReadLine());


            Product product = new()
            {
                Name = name,
                Price = price,
                Brand = brandName,
                TypesId = type
            };

            repo.AddEntity(product);            

            Console.WriteLine($"Product Name: {name} - Brand: {brandName} - Type: {repo.FindTpyeById(type).Name} - Price: {price} Created");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        static void DeleteProduct()
        {
            Console.WriteLine("\t\t**Delete Product**");
            Console.WriteLine("Select af product to delete");

            foreach (var item in repo.GetProducts())
            {
                Console.WriteLine($"ProductId: {item.ProductId}) product: {item.Name} - Brand {item.Brand} - Type: {item.Types.Name} - Price: {item.Price} ");
            }
            int id = Convert.ToInt32(Console.ReadLine());

            Product product = repo.FindProductById(id);
            product.IsDeleted = true;

            repo.UpdateEntit(product);

            Console.WriteLine($"{product.Name} was deleted....");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        static void UpdateProduct()
        {
            Console.WriteLine("\t\t**Update Product**");
            Console.WriteLine("Select af product to update");

            foreach (var item in repo.GetProducts())
            {
                Console.WriteLine($"ProductId: {item.ProductId}) product: {item.Name} - Brand {item.Brand} - Type: {item.Types.Name} - Price: {item.Price} ");
            }
            int id = Convert.ToInt32(Console.ReadLine());

            Product product = repo.FindProductById(id);

            Console.Write("Productnavn: ");
            string name = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Brand: ");
            string brandName = Console.ReadLine();

            string oldName = product.Name;

            product.Name = name;
            product.Price = price;
            product.Brand = brandName;

            repo.UpdateEntit(product);

            Console.WriteLine($"{oldName} was updated....");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        static void GetProducts()
        {
            Console.WriteLine("\t\t**List of Products**");

            foreach (var item in repo.GetProducts())
            {
                Console.WriteLine($"ProductId: {item.ProductId} product: {item.Name} - Brand: {item.Brand} - Type: {item.Types.Name} - Price: {item.Price}");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        static void GetProductsByBrand()
        {
            Console.WriteLine("\t\t**Get Products By Brand**");
            Console.Write("Type brand name: ");
            string brand = Console.ReadLine();

            List<Product> list = repo.GetProductByBrand(brand);

            if (list == null) 
            {
                Console.WriteLine($"Der er ikke noget brand ved navn {brand}");
            }

            foreach (var item in list)
            {
                Console.WriteLine($"ProductId {item.ProductId} - Product: {item.Name} - Price: {item.Price}");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
