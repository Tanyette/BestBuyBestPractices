using BestBuyBestPractices;
using Dapper;
using DapperInClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;

        }

        public void CreateProduct(string name, double price, int categoryID)
        {

            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) " +
                               "VALUES (@name, @price, @categoryID);",
                new { name = name, price = price, categoryID = categoryID });

        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _connection.Query<Products>("SELECT * FROM Products;");

        }

        
        public void UpdateProductName(int productID, string updatedName) 
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new {productID = productID, updatedName = updatedName});
            Console.WriteLine();
            Console.WriteLine($"product # {productID} updated");
            Thread.Sleep(3000);
        }
            
        public void DeleteProduct(int productID) 
        {
            _connection.Execute("DELETE FROM products WHERE productID = @productID;",
               new { productID = productID });
           // _connection.Execute("DELETE FROM sales WHERE productID = @productID;",
            //   new { productID = productID });
           // _connection.Execute("DELETE FROM reviews WHERE productID = @productID;",
              // new { prodID = productID });
            Console.WriteLine();
            Console.WriteLine($"product # {productID} was deleted.");
            Thread.Sleep(3000);

            var prodRepo = new DapperProductRepository(_connection);
            var prodList = prodRepo.GetAllProducts(); 
            
            foreach (var product in prodList) 
            {
                Console.WriteLine($"{product.ProductID} - {product.Name} - {product.Price}");
            }
        } 
    }
}
