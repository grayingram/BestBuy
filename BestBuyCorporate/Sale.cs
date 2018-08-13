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
        public double Price { get; private set; }
        public DateTime date { get; private set; }
    }
}
