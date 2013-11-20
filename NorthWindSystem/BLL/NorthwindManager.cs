using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity; // For use of .Include() extension method
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        #region Query Methods
        public List<NorthwindSystem.DataModels.HumanResources.Region> GetRegions()
        {
            using (var dbContext = new NorthwindSystem.DataModels.HumanResources.NorthwindHumanResources())
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
        public List<NorthwindSystem.DataModels.HumanResources.Employee> GetEmployees()
        {
            var dbContext = new NorthwindSystem.DataModels.HumanResources.NorthwindHumanResources();
            List<NorthwindSystem.DataModels.HumanResources.Employee> employees = dbContext.Employees.ToList();
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
