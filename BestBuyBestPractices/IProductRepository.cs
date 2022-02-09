using BestBuyBestPractices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DapperInClass 
{
    public interface IProductRepository
    {
        public IEnumerable<Products> GetAllProducts(); //method// 
        public void CreateProduct(string name, double price, int categoryID);


    }

        



    
}
