using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BestBuyCorporate
{
    class Deleter
    {
        public string ConnStr { get; private set; }
        public Reader reader = new Reader();
        //public Deleter MyDeleter = new Deleter();
        public Deleter()
        {
            ConnStr = "";
        }
        public Deleter(string connStr)
        {
            ConnStr = connStr;
            reader.SetConnStr(connStr);
            
        }
        public void SetConnStr(string connStr)
        {
            ConnStr = connStr;
        }
        //category has category id and name
        public void DeleteCategoryByName(string category)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM categories WHERE name = @category";
                cmd.Parameters.AddWithValue("category", category);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteCategoryById(int categoryID)
        {
            DeleteProductByCategory(categoryID);
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM categories WHERE categoryID = @categoryID";
                cmd.Parameters.AddWithValue("categoryID", categoryID);
                cmd.ExecuteNonQuery();
            }
        }
        
        
        //products has product id, name, price, category id
        public void DeleteProductByName(string product)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE name = @product";
                cmd.Parameters.AddWithValue("product", product);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteProductByID(int productID)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE productID = @productID";
                cmd.Parameters.AddWithValue("productID", productID);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteProductByPrice(decimal price)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE price = @price";
                cmd.Parameters.AddWithValue("price", price);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteProductByCategory(int categoryID)
        {
            DeleteSaleByProduct(reader.GetProductIDFromCatID(categoryID.ToString()));
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE categoryID = @categoryID";
                cmd.Parameters.AddWithValue("categoryID", categoryID);
                cmd.ExecuteNonQuery();
            }
        }

        //sales has salesId, prod Id, quantity, price, and date
        public void DeleteSaleByID(string saleid)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM sales WHERE salesID = @sale";
                cmd.Parameters.AddWithValue("saleID", saleid);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSaleByDate(DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM sales WHERE date = @date";
                cmd.Parameters.AddWithValue("date", date);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSaleByQuantity(int quantity)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM sales WHERE quantity = @quantity";
                cmd.Parameters.AddWithValue("quantity", quantity);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSaleByPrice(decimal price)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM sales WHERE price = @price";
                cmd.Parameters.AddWithValue("price", price);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSaleByProduct(int productid)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM sales WHERE productID = @productID";
                cmd.Parameters.AddWithValue("productID", productid);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
