using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindSystem.CBOs
{
    public class ProductSaleSummary
    {
        public decimal TotalSales { get; set; }
        public decimal TotalDiscount { get; set; }
        public int SaleCount { get; set; }
        public int SaleQuantity { get; set; }
        public decimal AverageUnitPrice { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public decimal CurrentUnitPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductId { get; set; }
    }
}
