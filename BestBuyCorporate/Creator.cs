using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BestBuyCorporate
{
    class Creator
    {
        public string ConnStr { get; set; }
        public Reader reader = new Reader();
        public Creator(string connStr)
        {
            ConnStr = connStr;
            reader.SetConnStr(connStr);
        }

        public void AddCategory(string name)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO category (Name) VALUES(@name);";
                cmd.Parameters.AddWithValue("name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddProduct(string name, decimal price, string category)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            int categoryID = reader.GetCategoryID(category);
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO products (Name, Price, CategoryID) VALUES(@name, @price, @categoryID);";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("categoryID", categoryID);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddSale(string product, int quantity, string date)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            decimal price = reader.GetPrice(product);
            int productID = reader.GetProductID(product);
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO sales (ProductID, Quantity, CategoryID) VALUES(@productID, @quantity, @price, @date);";
                cmd.Parameters.AddWithValue("productId", productID);
                cmd.Parameters.AddWithValue("quantity", quantity);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("date", date);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
