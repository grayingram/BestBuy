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
        public int GetCategoryID(int categoryId)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT CategoryID FROM categories as c WHERE  c.categoryID = @categoryId;";
                cmd.Parameters.AddWithValue("categoryId", categoryId);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int Product = int.Parse(dr[0].ToString());
                return Product;
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
        public string GetProductName(int productID)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Name FROM products as p WHERE p.productid = @productID;";
                cmd.Parameters.AddWithValue("productID", productID);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                string Product = dr[0].ToString();
                return Product;
            }
        }
        
        public int GetProductIDFromCatID(string categoryid)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            int product = GetCategoryID(categoryid);
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
        public List<int> GetProductsIDFromCatID(int categoryId)
        {
            List<int> productIds = new List<int>();
            MySqlConnection conn = new MySqlConnection(ConnStr);
            //int product = GetCategoryID(categoryId);
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID FROM products as p WHERE p.categoryID = @categoryId;";
                cmd.Parameters.AddWithValue("categoryID", categoryId);

                MySqlDataReader dr = cmd.ExecuteReader();
                //dr.Read();
                while (dr.Read())
                {
                    productIds.Add(int.Parse(dr["ProductID"].ToString()));
                }
                return productIds;
            }
        }
        public decimal GetProductPrice(string product)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Price FROM products as p WHERE p.Name = @product;";
                cmd.Parameters.AddWithValue("product", product);

                MySqlDataReader dr = cmd.ExecuteReader();
                decimal Price = 0;
                if (dr.Read()) {
                   Price = decimal.Parse(dr[0].ToString());
                }
                return Price;
            }
        }
        public List<string> GetProductsByPrice(decimal price)
        {
            List<string> products = new List<string>();
            MySqlConnection conn = new MySqlConnection(ConnStr);
            //int product = GetCategoryID(categoryId);
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Name FROM products as p WHERE p.Price = @price;";
                cmd.Parameters.AddWithValue("price", price);

                MySqlDataReader dr = cmd.ExecuteReader();
                //dr.Read();
                while (dr.Read())
                {
                    products.Add(dr["Name"].ToString());
                }
                return products;
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
        public bool DoesProductIdExist(int productid)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(p.productid) AS result FROM products p WHERE productid = @productid;";
                cmd.Parameters.AddWithValue("productid", productid);

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
        public bool DoesProductPriceExist(decimal productprice)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(p.price) AS result FROM products p WHERE price = @productprice;";
                cmd.Parameters.AddWithValue("productprice", productprice);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DoesSaleByPriceExist(decimal price)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Count(p.price) AS result FROM sales p WHERE price = @price;";
                cmd.Parameters.AddWithValue("price", price);

                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int count = int.Parse(dr[0].ToString());
                if (count >= 1)
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
