using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWindSystem.DAL;

namespace NorthWindSystem.BLL
{
    [DataObject]
    public class NorthwindManager
    {
        private NorthwindExtendedEntities DbContext { get; set; }

        public NorthwindManager()
        {
            DbContext = new NorthwindExtendedEntities();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Customer> GetCustomers()
        {
            return DbContext.Customers.ToList<Customer>();
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Employee> GetEmployees()
        {
            return DbContext.Employees.ToList<Employee>();
        }
    }
}
