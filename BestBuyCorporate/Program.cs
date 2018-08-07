﻿using System;
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

            Deleter deleter = new Deleter(connStr);

            if(Lawyer.GetYesNo("Would you like to create records?"))
            {
                Create(creator, reader);
            }

            if (Lawyer.GetYesNo("Would you like to delete records?"))
            {
                Delete(creator, deleter, reader);
            }
            
            Console.ReadLine();           
        }

        public static void Create(Creator creator, Reader reader)
        {
            do
            {
                if (Lawyer.GetYesNo("Would you like to add a category to the merchandise to sell?"))
                {
                    string category = Lawyer.GetResponse("What would you like to call this category to be added to the database?");
                    while (reader.DoesCategoryNameExist(category))
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
                    while (!(reader.DoesCategoryNameExist(category)))
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
                    int year = Lawyer.GetInt("What year has this transaction taken place?");
                    int month = Lawyer.GetInt("What numerical month has this transaction taken place?");
                    int day = Lawyer.GetInt("What day of the month has this transaction taken place?");
                    DateTime date = new DateTime(year, month, day);
                
                    creator.AddSale(product, quantity, date);
                }
            } while (Lawyer.GetYesNo("Do you want to add more records?"));
        }
        public static void Delete(Creator creator, Deleter deleter, Reader reader)
        {
            do
            {
                if (Lawyer.GetYesNo("Do you want to delete a category?"))
                {
                    if(Lawyer.GetYesNo("Do you want to delete a category by CategoryID"))
                    {
                        int catId = Lawyer.GetInt("What is the Category Id you want to delete?");
                        while (!(reader.DoesCategoryIdExist(catId)))
                        {
                            Console.WriteLine("Sorry that category does not exist, try again?");
                            catId = Lawyer.GetInt("What is the Category Id you want to delete ? ");
                        }
                        string categoryId = reader.GetCategoryName(catId);
                        if(Lawyer.GetYesNo("Are you sure you want to delete this category " + categoryId))
                        {
                            deleter.DeleteCategoryById(catId);
                        }

                    }
                    else if (Lawyer.GetYesNo("Do you want to delete a category by Name"))
                    {

                    }
                }
                else if (Lawyer.GetYesNo("Do you want to delete a product"))
                {
                    Console.WriteLine("Deleted product");
                }
                else if (Lawyer.GetYesNo("Do you want to delete a sale?"))
                {
                    Console.WriteLine("Deleted sale");
                }
            } while (Lawyer.GetYesNo("Do you want to delete more records?"));
        }
    }
}
