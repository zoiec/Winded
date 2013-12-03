using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity; // For use of .Include() extension method
using System.Text;
using System.Threading.Tasks;
using HR = NorthwindSystem.DataModels.HumanResources;
using NorthWindSystem.CBOs;

namespace NorthWindSystem.BLL
{
    [DataObject]
    public class NorthwindManager
    {
        #region Sales
        #region Command Methods
        #endregion

        #region Query Methods
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<NorthwindSystem.DataModels.Sales.Customer> GetCustomers()
        {
            var dbContext = new NorthwindSystem.DataModels.Sales.NorthwindSales();
            return dbContext.Customers.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Thanks to Matteo Tontini's bloq post "Linq To Entities:  Queryable.Sum returns Null on an empty list".
        /// <see cref="http://ilmatte.wordpress.com/2012/12/20/queryable-sum-on-decimal-and-null-return-value-with-linq-to-entities/"/>
        /// </remarks>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CustomerOrderSummary> GetCustomerOrderSummaries()
        {
            var dbContext = new NorthwindSystem.DataModels.Sales.NorthwindSales();
            var data = (from purchase in dbContext.Orders
                        where purchase.OrderDate.HasValue
                        select new CustomerOrderSummary()
                        {
                            OrderDate = purchase.OrderDate.Value,
                            Freight = purchase.Freight.GetValueOrDefault(),
                            Subtotal = purchase.Order_Details.Sum(x => (decimal?)(x.UnitPrice * x.Quantity)) ?? 0,
// TODO: Fix this after the fix for GetProductSaleSummaries
                            //Discount = purchase.Order_Details.Sum(x => x.UnitPrice * x.Quantity * (decimal)x.Discount),
                            //Total = purchase.Order_Details.Sum(x => (x.UnitPrice * x.Quantity) -
                            //                                        (x.UnitPrice * x.Quantity * (decimal)x.Discount)),
                            ItemCount = purchase.Order_Details.Count(),
                            ItemQuantity = purchase.Order_Details.Sum(x => (short?)x.Quantity) ?? 0,
                            AverageItemUnitPrice = purchase.Order_Details.Average(x => (decimal?)x.UnitPrice) ?? 0,
                            CompanyName = purchase.Customer.CompanyName,
                            ContactName = purchase.Customer.ContactName,
                            ContactTitle = purchase.Customer.ContactTitle,
                            CustomerId = purchase.CustomerID
                        }).ToList();
            return data;
        }

        private class SimpleCategory
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Thanks to Matteo Tontini's bloq post "Linq To Entities:  Queryable.Sum returns Null on an empty list".
        /// <see cref="http://ilmatte.wordpress.com/2012/12/20/queryable-sum-on-decimal-and-null-return-value-with-linq-to-entities/"/>
        /// </remarks>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProductSaleSummary> GetProductSaleSummaries()
        {
            // NOTE: Casting to Decimal is not supported in LINQ to Entities queries, because the required precision and scale information cannot be inferred.
            var dbContext = new NorthwindSystem.DataModels.Sales.NorthwindSales();
            var data = (from item in dbContext.Products
                        where !item.Discontinued
                        select new ProductSaleSummary()
                        {
                            TotalSales = item.Order_Details.Sum(x => (decimal?)(x.UnitPrice * x.Quantity)) ?? 0,

                            /* NOTE:
                             *  Having a problem with casting float to decimal in L2E:
                             *      "Casting to Decimal is not supported in LINQ to Entities queries, 
                             *       because the required precision and scale information cannot be inferred."
                             *  Cast is happening in the generated query, not in memory. It looks like I might have to
                             *  create a function on the data model or something to handle this.
                             *  
                             *  BTW:
                             *  It's necessary to calculate discounts applied on a row-by-row basis BEFORE doing a sum
                             *  in order to get the correct amount.
                             *  (e.g.:  You get a different calculation if you sum before multiplying:
                             *          5 items X $ 10.00  = $ 50.00
                             *        +
                             *          2 items X $  0.50  = $  1.00
                             *          ----------------------------
                             *                             = $ 51.00    <== CORRECT answer
                             *                             
                             *     vs.
                             *          5 items X $ 10.00
                             *        +
                             *          2 items X $  0.50
                             *          ----------------------------
                             *          7 items X $ 10.50   = $ 73.50   <== WRONG answer
                             *  )
                             */

                            //TotalDiscount = (from detail in item.Order_Details
                            //                 select new // anonymous type from DB
                            //                 {
                            //                     UnitPrice = detail.UnitPrice,
                            //                     Quantity = detail.Quantity,
                            //                     Discount = detail.Discount
                            //                 }).AsEnumerable() // perform rest of work in memory
                            //                 .Sum(x => x.UnitPrice * x.Quantity * (decimal)x.Discount),


                            //TotalDiscount = item.Order_Details
                            ////.Select<NorthwindSystem.DataModels.Sales.Order_Detail, decimal>(x => x.)
                            //                .Sum(x => 
                            //                          (decimal?)(x.UnitPrice * x.Quantity * (decimal)x.Discount)) ?? 0,
                            //                          //(float?)x.Discount
                            //                          //(decimal?)x.UnitPrice * (short?)x.Quantity * Convert.ToDecimal((float?)x.Discount)) ?? decimal.Zero, // 0M,




                            SaleCount = item.Order_Details.Count(),
                            SaleQuantity = item.Order_Details.Sum(x => (short?)x.Quantity) ?? 0,
                            AverageUnitPrice = item.Order_Details.Average(x => (decimal?)x.UnitPrice) ?? 0,
                            ProductName = item.ProductName,
                            QuantityPerUnit = item.QuantityPerUnit,
                            UnitsInStock = item.UnitsInStock.HasValue ?
                                           item.UnitsInStock.Value : (short)0,
                            UnitsOnOrder = item.UnitsOnOrder.HasValue ?
                                           item.UnitsOnOrder.Value : (short)0,
                            ReorderLevel = item.ReorderLevel.HasValue ?
                                           item.ReorderLevel.Value : (short)0,
                            Discontinued = item.Discontinued,
                            CurrentUnitPrice = item.UnitPrice.HasValue ?
                                           item.UnitPrice.Value : 0,
                            CategoryId = item.CategoryID.HasValue ?
                                           item.CategoryID.Value : 0,
                            ProductId = item.ProductID
                        }).ToList();

            var dbInventoryContext = new NorthwindSystem.DataModels.Purchasing.NorthwindPurchasing();
            foreach (var item in data)
                if (item.CategoryId > 0)
                    item.CategoryName = dbInventoryContext.Categories.Find(item.CategoryId).CategoryName;
            return data;
        }
        #endregion
        #endregion


        #region Human Resources
        #region Command Methods
        public int Add(HR.Region region)
        {
            if (region == null)
                throw new ArgumentNullException("region", "region is null.");
            using (var dbContext = new HR.NorthwindHumanResources())
            {
                /* NOTE:
                 *  The TerritoryID column in Territories is a string - nvarchar(20) - rather than an integer.
                 *  The existing data in Northwind Traders uses the zip code of the city/town as the TerritoryID.
                 *  This sample just "simplifies" and assigns the territory description as the ID, since we're
                 *  in Canada and we aren't using a single zip or postal code.
                 */
                foreach (var territory in region.Territories)
                    if (string.IsNullOrEmpty(territory.TerritoryID))
                        territory.TerritoryID = territory.TerritoryDescription;

                /* NOTE:
                 *  The RegionID column in Regions is an integer, but it is not an IDENTITY column.
                 *  As such, we're simply going to get the next highest ID available.
                 */
                if(region.RegionID <= 0)
                    region.RegionID = dbContext.Regions.Max(item => item.RegionID) + 1;

                dbContext.Regions.Add(region);

                dbContext.SaveChanges();

                return region.RegionID;
            }
        }

        public void Update(NorthwindSystem.DataModels.HumanResources.Region region, List<NorthwindSystem.DataModels.HumanResources.Territory> territories)
        {
            if (region == null)
                throw new ArgumentNullException("region", "region is null.");
            if (territories == null)
                throw new ArgumentNullException("territories", "territories is null.");

            using (var dbContext = new HR.NorthwindHumanResources())
            {
                foreach (var item in territories)
                {
                    var found = dbContext.Territories.Find(item.TerritoryID);
                    if (found != null)
                    {
                        /* NOTE:
                         *  Pre-process the Territory IDs to see if they should be "synced" with the name/description.
                         *  This will be the case if, in the original, the ID was the same as the description
                         */
                        string foundTerritoryID = found.TerritoryID;
                        string foundTerritoryDescription = found.TerritoryDescription.Trim(); // HACK: Turns out, the column is nchar(50), not an nvarchar....
                        string itemTerritoryID = item.TerritoryID;
                        string itemTerritoryDescription = item.TerritoryDescription.Trim();
                        if (foundTerritoryID.Equals(foundTerritoryDescription) &&
                            !itemTerritoryID.Equals(itemTerritoryDescription))
                        {
                            item.TerritoryID = itemTerritoryDescription;
                            dbContext.Territories.Remove(found); // Because the PK has changed...
                            dbContext.Territories.Add(item); // Because the PK has changed...
                        }
                    }
                }

                dbContext.Entry(region).State = System.Data.EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
        #endregion

        #region Query Methods
        public List<HR.Region> GetRegions()
        {
            using (var dbContext = new HR.NorthwindHumanResources())
            {
                var regions = dbContext.Regions
                                       .Include(item => item.Territories)
                                       .OrderBy(item => item.RegionDescription);

                foreach (var region in regions)
                {
                    // TODO: See why the sorting of the Territories isn't working right....
                    region.Territories.OrderBy(item => item.TerritoryDescription);
                }
                return regions.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<HR.Employee> GetEmployees()
        {
            var dbContext = new HR.NorthwindHumanResources();
            List<HR.Employee> employees = dbContext.Employees.ToList();
            return employees;
        }

        public List<NorthwindSystem.DataModels.Sales.Order> GetOrders()
        {
            var dbContext = new NorthwindSystem.DataModels.Sales.NorthwindSales();
            return dbContext.Orders.ToList();
        }

        public string EmployeePerformance(int? empId)
        {
            string review;
            var shortList = GetOrders().Where(item => item.EmployeeID == empId).ToList();
            if (shortList.Count > 0)
            {
                int distinctCustomers = (from item in shortList
                                         select item.CustomerID).Distinct().ToList().Count;
                review = string.Format("{0} sales for {1} customers.", shortList.Count, distinctCustomers);
            }
            else
            {
                review = "No direct customer sales";
            }
            return review;
        }
        #endregion
        #endregion
    }
}
