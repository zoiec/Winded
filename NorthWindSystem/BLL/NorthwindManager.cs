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

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<NorthWindSystem.DataModels.Sales.Customer> GetCustomers()
        {
            var dbContext = new NorthWindSystem.DataModels.Sales.NorthwindSales();
            return dbContext.Customers.ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<NorthWindSystem.DataModels.HumanResources.Employee> GetEmployees()
        {
            var dbContext = new NorthWindSystem.DataModels.HumanResources.NorthwindHumanResources();
            List<NorthWindSystem.DataModels.HumanResources.Employee> employees = dbContext.Employees.ToList();
            return employees;
        }

        public List<DataModels.Sales.Order> GetOrders()
        {
            var dbContext = new NorthWindSystem.DataModels.Sales.NorthwindSales();
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
    }
}
