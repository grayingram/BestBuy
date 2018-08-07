using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BestBuyCorporate
{
    class Deleter
    {
        public string ConnStr { get; set; }
        public Reader reader = new Reader();
        public Deleter()
        {
            ConnStr = "";
        }
        public Deleter(string connStr)
        {
            ConnStr = connStr;
            reader.SetConnStr(connStr);
        }
        //category has category id and name
        public void DeleteCategoryByName(string category)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE Name FROM catergories WHERE name = @category";
                cmd.Parameters.AddWithValue("category", category);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteCategoryById(int categoryID)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE Name FROM catergories WHERE categoryID = @categoryID";
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
        //sales has salesId, prod Id, quantity, price, and date
        public void DeleteSaleByID(string saleid)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE Name FROM sales WHERE SalesID = @sale";
                cmd.Parameters.AddWithValue("saleID", saleid);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
