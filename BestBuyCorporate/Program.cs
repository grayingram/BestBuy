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
        public static Lawyer Lawyer { get; set; } = new Lawyer();
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
            
            Reader reader = new Reader(connStr);

            if(Lawyer.GetYesNo("Whould you like to create records?"))
            {
                Create(creator, reader);
            }
            
            Console.ReadLine();           
        }

        public static void Create(Creator creator, Reader reader)
        {
            do
            {
                if (Lawyer.GetYesNo("Would you like to add a category to the merchendise to sell?"))
                {
                    string category = Lawyer.GetResponse("What would you like to call this category to be added to the database?");
                    while (reader.DoesCategoryExist(category))
                    {
                        Console.WriteLine("Sorry that category already exist try again?");
                        category = Lawyer.GetResponse("What would you like to call this category to be added to the database?");
                    }
                    creator.AddCategory(category);
                }

                if (Lawyer.GetYesNo("Would you like to add a new product?"))
                {
                    string name = Lawyer.GetResponse("What is the name of the item to be added to the store?");
                    string category = Lawyer.GetResponse("What category do you want would this item be considered?");
                    while (!(reader.DoesCategoryExist(category)))
                    {
                        Console.WriteLine("Category does not exist");
                        category = Lawyer.GetResponse("What category do would you consider this item?");
                    }
                    //previous line could break the entire program thinking about another add on to lawyer class
                    decimal price = Lawyer.GetDecimal("How much does this product cost?");
                    creator.AddProduct(name, price, category);
                }

                if (Lawyer.GetYesNo("Would you like to add a sale?"))
                {
                    string product = Lawyer.GetResponse("What is the name of the product that has been sold?");
                    while (!(reader.DoesProductNameExist(product)))
                    {
                        Console.WriteLine("That product does not exist, please enter a valid product.");
                        product = Lawyer.GetResponse("What is the name of the product that has been sold?");
                    }
                    int quantity = Lawyer.GetInt("How many of the item did the customer buy?");
                    string date = Lawyer.GetResponse("What day has this transaction taken place?");
                    creator.AddSale(product, quantity, date);
                }
            } while (Lawyer.GetYesNo("Do you want to add more records?"));
        }
    }
}
