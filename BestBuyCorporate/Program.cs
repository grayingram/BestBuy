using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Text;
namespace BestBuyCorporate

{
    class Program
    {
        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();
            string connStr = configBuilder.GetConnectionString("DefaultConnection");

            Creator creator = new Creator(connStr);
            Lawyer lawyer = new Lawyer();
            Reader reader = new Reader(connStr);

            if(lawyer.GetYesNo("Would you like to add a category to the merchendise to sell?"))
            {
                string category = lawyer.GetResponse("What would you like to call this category to be added to the database?");
                while(reader.DoesCategoryExist(category))
                {
                    Console.WriteLine("Sorry that category already exist try again?");
                    category = lawyer.GetResponse("What would you like to call this category to be added to the database?");
                }
                creator.AddCategory(category);
                
                
            }

            if(lawyer.GetYesNo("Would you like to add a new product?"))
            {
                string name = lawyer.GetResponse("What is the name of the item to be added to the store?");
                string category = lawyer.GetResponse("What category do you want would this item be considered?");
                //previous line could break the entire program thinking about another add on to lawyer class
                decimal price = lawyer.GetDecimal("How much does this product cost?");
                creator.AddProduct(name, price, category);
            }

            if(lawyer.GetYesNo("Would you like to add a sale?"))
            {
                string product = lawyer.GetResponse("What is the name of the product that has been sold?");
                int quantity = lawyer.GetInt("How many of the item did the customer buy?");
                string date = lawyer.GetResponse("What day has this transaction taken place?");
                creator.AddSale(product, quantity, date);
            }

            Console.ReadLine();

            

            
        }
    }
}
