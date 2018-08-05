using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BestBuyCorporate
{
    class Reader
    {
        public string ConnStr { get; set; }
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

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT CatergoryID FROM categories as c WHERE c.Name = @category;";
                cmd.Parameters.AddWithValue("category", category);

                MySqlDataReader dr = cmd.ExecuteReader();

                int Category = int.Parse(dr[0].ToString());
                return Category;
            }
        }

        public int GetProductID(string product)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT CatergoryID FROM products as c WHERE c.Name = @product;";
                cmd.Parameters.AddWithValue("product", product);

                MySqlDataReader dr = cmd.ExecuteReader();

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

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT Price FROM products as c WHERE c.Name = @product;";
                cmd.Parameters.AddWithValue("product", product);

                MySqlDataReader dr = cmd.ExecuteReader();

                int Price = int.Parse(dr[0].ToString());
                return Price;
            }
        }

        public bool DoesCategoryExist(string category)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT Count(c.name) AS result FROM categories c WHERE name = @category;";
                cmd.Parameters.AddWithValue("category", category);

                MySqlDataReader dr = cmd.ExecuteReader();

                int count = int.Parse(dr[0].ToString());
                if(count == 1)
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
