using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCorporate
{
    class Category
    {
        public string Name { get; private set; }
        public int CategoryId { get; private set; }

        public Category(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }
    }
}
