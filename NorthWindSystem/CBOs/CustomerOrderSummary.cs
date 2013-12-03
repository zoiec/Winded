using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindSystem.CBOs
{
    public class CustomerOrderSummary
    {
        public DateTime OrderDate { get; set; }
        public decimal Freight { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
        public int ItemQuantity { get; set; }
        public decimal AverageItemUnitPrice { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string CustomerId { get; set; }
    }
}