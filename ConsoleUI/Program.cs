using Microsoft.EntityFrameworkCore;
using DataLayer;
using System;
using ServiceLayer.Repository;
using ServiceLayer.Interface;
using DataLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;

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
            Menu();
            string read = Console.ReadLine();
            Console.Clear();

            switch (read)
            {
                default:
                    break;
                case "1":
                    CreateProduct();
                    break;
                case "2":
                    DeleteProduct();
                    break;
                case "3":
                    UpdateProduct();
                    break;
                case "4":
                    GetProducts();
                    break;
                case "5":
                    GetProductsByBrand();
                    break;
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
            Console.WriteLine("------------------------------------------------------------");
        }

        static void CreateProduct()
        {
            Console.WriteLine("\t\t**Create Product**");
            Console.WriteLine("Productnavn: ");
            string name = Console.ReadLine();

            Console.WriteLine("Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Brand: ");
            string brandName = Console.ReadLine();

            int i = 1;

            foreach (var item in repo.GetTypes())
            {
                Console.WriteLine("Select a type");
                Console.WriteLine($"{i}) Type: {item.Name}");
                i++;
            }
            int type = Convert.ToInt32(Console.ReadLine());


            Product product = new()
            {
                Name = name,
                Price = price,
                Brand = brandName,
                TypesId = type
            };

            repo.AddEntity(product);

            Console.WriteLine($"Product Name: {name} - Brand: {brandName} - Type: {type} - Price: {price} Created");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        static void DeleteProduct()
        {
            Console.WriteLine("\t\t**Delete Product**");
            Console.WriteLine("Select af product to delete");

            int i = 1;
            foreach (var item in repo.GetProducts())
            {
                Console.WriteLine($"{i}) product: {item.Name} - Brand {item.Brand} - Type: {item.Types.Name} - Price: {item.Price} ");
            }
            int id = Convert.ToInt32(Console.ReadLine());

            Product product = repo.FindById(id);
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

            int i = 1;
            foreach (var item in repo.GetProducts())
            {
                Console.WriteLine($"{i}) product: {item.Name} - Brand {item.Brand} - Type: {item.Types.Name} - Price: {item.Price} ");
            }
            int id = Convert.ToInt32(Console.ReadLine());

            Product product = repo.FindById(id);

            Console.WriteLine("Productnavn: ");
            string name = Console.ReadLine();

            Console.WriteLine("Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Brand: ");
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

            int i = 1;
            foreach (var item in repo.GetProducts())
            {
                Console.WriteLine($"{i} product: {item.Name} - Brand: {item.Brand} - Type: {item.Types.Name} - Price: {item.Price}");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        static void GetProductsByBrand()
        {
            Console.WriteLine("\t\t**Get Products By Brand**");
            Console.WriteLine("Type brand name: ");
            string brand = Console.ReadLine();

            List<Product> list = repo.GetProductByBrand(brand);

            if (list == null)
            {
                Console.WriteLine($"Der er ikke noget brand ved navn {brand}");

                foreach (var item in list)
                {
                    Console.WriteLine($"\t\t{brand}");
                    Console.WriteLine($"product: {item.Name} - Price: {item.Price}");
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
