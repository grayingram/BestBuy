using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCorporate
{
    class Product
    {
        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int CategoryId { get; private set; }

        public Product(int productid, string name, decimal price, int categoryid)
        {
            ProductId = productid;
            Name = name;
            Price = price;
            CategoryId = categoryid;
        }
    }
}
