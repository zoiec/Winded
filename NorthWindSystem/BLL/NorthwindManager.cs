using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity; // For use of .Include() extension method
using System.Text;
using System.Threading.Tasks;
using HR = NorthwindSystem.DataModels.HumanResources;

namespace NorthWindSystem.BLL
{
    [DataObject]
    public class NorthwindManager
    {
        #region Sales
        #region Command Methods
        #endregion

        #region Query Methods
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<NorthwindSystem.DataModels.Sales.Customer> GetCustomers()
        {
            var dbContext = new NorthwindSystem.DataModels.Sales.NorthwindSales();
            return dbContext.Customers.ToList();
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
