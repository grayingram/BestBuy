﻿using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BestBuyCorporate
{
    class Updater
    {
        public string ConnStr { get; private set; }
        public Reader reader = new Reader();
        public Updater()
        {
            ConnStr = "";
        }
        public Updater(string connStr)
        {
            ConnStr = connStr;
            reader.SetConnStr(connStr);
        }
        
        public void UpdateCategoryByName(string category, string change)
        {
            int catid = reader.GetCategoryID(category);
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update categories SET name = @change WHERE name = @category";
                cmd.Parameters.AddWithValue("category", category);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateCategoryById(int categoryid, string change)
        {
            string catname = reader.GetCategoryName(categoryid);
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update categories SET name = @change WHERE categoryid = @categoryid";
                cmd.Parameters.AddWithValue("categoryid", categoryid);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProductNameByName(string product, string change)
        {
            int prodid = reader.GetProductID(product);
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET name = @change WHERE name = @product";
                cmd.Parameters.AddWithValue("product", product);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateProductNameById(int prodid, string change)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET name = @change WHERE productid = @prodid";
                cmd.Parameters.AddWithValue("prodid", prodid);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateProductPriceByPrice(decimal price ,decimal change)
        {
            if (reader.DoesSaleByPriceExist(price))
            {
                updateSalePriceByPrice(price, change);
            }
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET price = @change WHERE price = @price";
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateProductPriceByName(string name, decimal change)
        {
            if (reader.DoesSaleByProdIdExist(reader.GetProductID(name)))
            {
                updateSaleByPriceProductId(reader.GetProductID(name), change);
            }
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET price = @change WHERE name = @name";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }
                               
        private void updateSalePriceByPrice(decimal price, decimal change)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE sales SET price = @change WHERE price = @price";
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }
        private void updateSaleByPriceProductId(int prodid, decimal change)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE sales SET price = @change WHERE prodid = @prodid";
                cmd.Parameters.AddWithValue("prodid", prodid);
                cmd.Parameters.AddWithValue("change", change);
                cmd.ExecuteNonQuery();
            }
        }



    }
}
