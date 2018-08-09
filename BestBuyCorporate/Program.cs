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

            Deleter deleter = new Deleter(connStr);

            Updater updater = new Updater(connStr);

            if(Lawyer.GetYesNo("Would you like to create records?"))
            {
                Create(creator, reader);
            }

            else if (Lawyer.GetYesNo("Would you like to delete records?"))
            {
                Delete(deleter, reader);
            }

            else if(Lawyer.GetYesNo("Would you like to update records?"))
            {
                Update(updater, reader);
                
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
        public static void Delete(Deleter deleter, Reader reader)
        {
            do
            {
                if (Lawyer.GetYesNo("Do you want to delete a category?"))
                {
                    DeleteCategory(deleter, reader);
                }
                else if (Lawyer.GetYesNo("Do you want to delete a product?"))
                {
                    DeleteProduct(deleter, reader);
                }
                else if (Lawyer.GetYesNo("Do you want to delete a sale?"))
                {
                    DeleteSale(deleter, reader);
                }
            } while (Lawyer.GetYesNo("Do you want to delete more records?"));
        }
        public static void DeleteCategory(Deleter deleter, Reader reader)
        {
            if (Lawyer.GetYesNo("Do you want to delete a category by CategoryID"))
            {
                int catId = Lawyer.GetInt("What is the Category Id you want to delete?");
                while (!(reader.DoesCategoryIdExist(catId)))
                {
                    Console.WriteLine("Sorry that category does not exist, try again.");
                    catId = Lawyer.GetInt("What is the Category Id you want to delete? ");
                }
                string categoryName = reader.GetCategoryName(catId);
                if (Lawyer.GetYesNo("Are you sure you want to delete this category " + categoryName))
                {
                    deleter.DeleteCategoryById(catId);
                }

            }
            else if (Lawyer.GetYesNo("Do you want to delete a category by Name"))
            {
                string category = Lawyer.GetResponse("What is the Name of the Category you want to delete?");
                while (!(reader.DoesCategoryNameExist(category)))
                {
                    Console.WriteLine("Sorry that category does not exist, try again.");
                    category = Lawyer.GetResponse("What is the Name of the Category you want to delete?");
                }
                if (Lawyer.GetYesNo("Are you sure you want to delete this category :" + category))
                {
                    deleter.DeleteCategoryByName(category);
                }
            }
        }
        public static void DeleteProduct(Deleter deleter, Reader reader)
        {
            if (Lawyer.GetYesNo("Do you want to delete a product by Product Id?"))
            {
                int prodid = Lawyer.GetInt("What is the Product id that you want to delete?");
                while (!(reader.DoesProductIdExist(prodid)))
                {
                    Console.WriteLine("Sorry but that product does not exist, try again.");
                    prodid = Lawyer.GetInt("What is the Product id that you want to delete?");
                }
                string productId = reader.GetProductName(prodid);
                if (Lawyer.GetYesNo("Are you sure you want to delete this product :" + productId))
                {
                    deleter.DeleteProductByID(prodid);
                }
            }
            else if (Lawyer.GetYesNo("Do you want to delete a product by Product Name?"))
            {
                string prodname = Lawyer.GetResponse("What is the Name of the product you want to delete?");
                while (!(reader.DoesProductNameExist(prodname)))
                {
                    Console.WriteLine("Sorry but that product does not exist, try again.");
                    prodname = Lawyer.GetResponse("What is the Name of the product you want to delete?");
                }
                if (Lawyer.GetYesNo("Are you sure you want to delete this product :" + prodname))
                {
                    deleter.DeleteProductByName(prodname);
                }
            }
            else if (Lawyer.GetYesNo("Do you want to delete a product by Price?"))
            {
                decimal prodprice = Lawyer.GetDecimal("What is the price of products you want to delete?");
                while (!(reader.DoesProductPriceExist(prodprice)))
                {
                    Console.WriteLine("Sorry no product has that price, try again.");
                    prodprice = Lawyer.GetDecimal("What is the price of products you want to delete?");
                }
                List<string> products = reader.GetProductsByPrice(prodprice);
                if (Lawyer.GetYesNo("Are you sure you want to delete " + products.Count + " products"))
                {
                    foreach (string product in products)
                    {
                        deleter.DeleteProductByPrice(reader.GetProductPrice(product));
                        
                    }
                }
            }
            else if (Lawyer.GetYesNo("Do you want to delete a product by Category Id?"))
            {
                int catid = Lawyer.GetInt("What is the Category Id you want to delete products by?");
                while (!(reader.DoesProductWithCatIdExist(catid)))
                {
                    Console.WriteLine("Sorry there are no products with that category id, try again");
                    catid = Lawyer.GetInt("What is the Category Id you want to delete products by?");
                }
                string category = reader.GetCategoryName(catid);
                if (Lawyer.GetYesNo("Are you sure you want to delete all products that are asscoiated with:" + category + "?"))
                {
                    deleter.DeleteProductByCategory(catid);
                }
            }
        }
        public static void DeleteSale(Deleter deleter, Reader reader)
        {
            if (Lawyer.GetYesNo("Do you want to delete a sale by Sale Id?"))
            {
                int saleid = Lawyer.GetInt("What Sale Id do you want to delete?");
                while (!(reader.DoesSaleIDExist(saleid)))
                {
                    Console.WriteLine("Sorry but no sale with that id exist, try again.");
                    saleid = Lawyer.GetInt("What Sale Id do you want to delete?");
                }
                if (Lawyer.GetYesNo("Are you sure you want to delete sale with id:" + saleid))
                {
                    deleter.DeleteSaleByID(saleid.ToString());
                }

            }
            else if (Lawyer.GetYesNo("Do you to delete a sale by Product Id?"))
            {
                int prodid = Lawyer.GetInt("By what Product id do you wnat to delete sales?");
                while (!(reader.DoesSaleByProdIdExist(prodid)))
                {
                    Console.WriteLine("Sorry but no sale with that product id exist, try again.");
                    prodid = Lawyer.GetInt("By what Product id do you want to delete sales?");
                }
                string productid = reader.GetProductName(prodid);
                if (Lawyer.GetYesNo("Are you sure you want to delete sales associated with the product: " + productid))
                {
                    deleter.DeleteSalesByProductId(prodid);
                }
            }
            else if (Lawyer.GetYesNo("Do you want to delete a sale by quantity?"))
            {
                int salequantity = Lawyer.GetInt("What quantity do you want to delete all sales of?");
                while (!(reader.DoesSaleByQuantityExist(salequantity)))
                {
                    Console.WriteLine("Sorry there are no sales with that quantity, try again.");
                    salequantity = Lawyer.GetInt("What quantity do you want to delete all sales of?");
                }
                if (Lawyer.GetYesNo("Are you sure you want to delete sales with quantity of: " + salequantity + "?"))
                {
                    deleter.DeleteSaleByQuantity(salequantity);
                }
            }
            else if (Lawyer.GetYesNo("Do you want to delete a sale by a date?"))
            {

                int month = Lawyer.GetMonth("What month do you want to delete sales by?");
                int year = Lawyer.GetYear("What year do you want to delete sales by?");
                int day = Lawyer.GetDay("What day do you want to delete sales by?", month, year);
                DateTime date = new DateTime(year, month, day);
                while (!(reader.DoesSaleByDateExist(date)))
                {
                    Console.WriteLine("Sorry but there are no sales with that date?");
                    month = Lawyer.GetMonth("What month do you want to delete sales by?");
                    year = Lawyer.GetYear("What year do you want to delete sales by?");
                    day = Lawyer.GetDay("What day do you want to delete sales by?", month, year);
                    DateTime dt = new DateTime(year, month, day);
                    date = dt;
                }
                if (Lawyer.GetYesNo("Are you sure you want to delete sales of date : Year " + date.Year.ToString() + " Month " + date.Month.ToString() + " Day " + date.Day.ToString() + "?"))
                {
                    deleter.DeleteSaleByDate(date);
                }
            }
            else if (Lawyer.GetYesNo("Do you want to delete sales by a certain price?"))
            {
                decimal price = Lawyer.GetDecimal("What is the price you want to delete sales by?");
                while (!(reader.DoesSaleByPriceExist(price)))
                {
                    Console.WriteLine("There are no sales with that price, try again.");
                    price = Lawyer.GetDecimal("What is the price you want to delete sales by?");
                }
                if (Lawyer.GetYesNo("Are you sure you want to delete sales with price: " + price))
                {
                    deleter.DeleteSaleByPrice(price);
                }
            }
        }
        public static void Update(Updater updater, Reader reader)
        {
            do
            {
                if (Lawyer.GetYesNo("Do you want to update a category name?"))
                {
                    if (Lawyer.GetYesNo("Do you want to update a category name using its name?"))
                    {
                        string currentName = Lawyer.GetResponse("What is the category you want to rename?");
                        while (!(reader.DoesCategoryNameExist(currentName)))
                        {
                            Console.WriteLine("Sorry but that there is no category by that name, try again.");
                            currentName = Lawyer.GetResponse("What is the category you want to rename?");
                        }
                        string newName = Lawyer.GetResponse("What do you want to rename the category?");
                        while (reader.DoesCategoryNameExist(newName))
                        {
                            Console.WriteLine("Sorry but that category name already exists, try again.");
                            newName = Lawyer.GetResponse("What do you want to rename the category?");
                        }
                        if (Lawyer.GetYesNo("Are you sure you want to rename the category: " + currentName + " to " + newName + "?"))
                        {
                            updater.UpdateCategoryByName(currentName, newName);
                        }
                    }
                    else if (Lawyer.GetYesNo("Do you want to update a category name using its id?"))
                    {

                    }
                }
            } while (Lawyer.GetYesNo("Do you want to update more records?"));
        }
    }
}
