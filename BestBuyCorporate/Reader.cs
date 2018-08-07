using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BestBuyCorporate
{
    class Reader
    {
        public string ConnStr { get; private set; }
        public Reader()
        {
            ConnStr = "";
        }

        public Reader(string connStr)
        {
            ConnStr = connStr;
        }
        public void SetConnStr(string connStr)
        {
            ConnStr = connStr;
        }

        public int GetCategoryID(string category)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT CategoryID FROM categories as c WHERE c.Name = @category;";
                cmd.Parameters.AddWithValue("category", category);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int Category = int.Parse(dr[0].ToString());
                return Category;
            }
        }
        public string GetCategoryName(int categoryID)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Name FROM categories as c WHERE c.CategoryID = @categoryID;";
                cmd.Parameters.AddWithValue("categoryID", categoryID);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                string Category = dr[0].ToString();
                return Category;
            }
        }

        public int GetProductID(string product)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID FROM products as p WHERE p.Name = @product;";
                cmd.Parameters.AddWithValue("product", product);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int Product = int.Parse(dr[0].ToString());
                return Product;
            }
        }

        public decimal GetPrice(string product)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Price FROM products as p WHERE p.Name = @product;";
                cmd.Parameters.AddWithValue("product", product);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                decimal Price = decimal.Parse(dr[0].ToString());
                return Price;
            }
        }

        public bool DoesCategoryNameExist(string category)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(c.name) AS result FROM categories c WHERE name = @category;";
                cmd.Parameters.AddWithValue("category", category);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public bool DoesProductNameExist(string product)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(p.name) AS result FROM products p WHERE name = @product;";
                cmd.Parameters.AddWithValue("product", product);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DoesCategoryIdExist(int categoryID)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(c.categoryID) AS result FROM categories c WHERE categoryID = @categoryId;";
                cmd.Parameters.AddWithValue("categoryId", categoryID);


                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
