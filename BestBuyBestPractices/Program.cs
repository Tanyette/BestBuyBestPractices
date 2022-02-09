using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using DapperInClass;

namespace BestBuyBestPractices
{
    class Program
    {
        

        public static void Main(string[] args)
        {
            
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperDepartmentRepository(conn);


            Console.WriteLine("Please enter new Department name.");
            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);          
                
            var Departments = repo.GetAllDepartments();

            foreach (var department in Departments) 
            {
                Console.WriteLine(department.Name);
            }


            Console.WriteLine("what is the name of the product?");
            var prodName = Console.ReadLine();
            Console.WriteLine("what is the price?");
            var prodPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("what is the category ID ?");
            var prodCat = int.Parse(Console.ReadLine());
            var productRepo = new DapperProductRepository(conn);
            //productRepo.CreateProduct(prodName, prodPrice, prodCat);
            //var prodList = productRepo.GetAllProducts();

            //foreach (var product in prodList) 
            //{
            //    Console.WriteLine($"{prod.ProductID} - {prod.Name}");
           // }

            //update product
            Console.WriteLine("what is the Product ID you want to update?");
            var prodID = int.Parse(Console.ReadLine());

            var prodNewName = Console.ReadLine();

            productRepo.UpdateProductName(prodID, prodNewName);

            var prodList = productRepo.GetAllProducts();

            foreach (var product in prodList)
            {
                Console.WriteLine($"{product.ProductID} - {product.Name} - {product.Price}");
            }

            Console.WriteLine("what is the Product ID you want to delete?");
            var productID = int.Parse(Console.ReadLine());

            productRepo.DeleteProduct(productID); 

        }
    }
}
