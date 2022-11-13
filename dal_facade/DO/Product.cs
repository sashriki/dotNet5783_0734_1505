using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int AmmountInStock { get; set; }
        public double ProductPrice { get; set; }
        public Category ProductCategory { get; set; }

        public override string ToString() => $@"
         Product ID: {ProductId} - {ProductName}
         category: {ProductCategory}
         Price: {ProductPrice}
         Amount in stock: {AmmountInStock}";
    }
}
