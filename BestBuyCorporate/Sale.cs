using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCorporate
{
    class Sale
    {
        public int SaleId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public DateTime Date { get; private set; }

        public Sale(int saleid, int productid, int quantity, decimal price, DateTime date)
        {
            SaleId = saleid;
            ProductId = productid;
            Quantity = quantity;
            Price = price;
            Date = date;
        }
    }
}
