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
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Customer> GetCustomers()
        {
            NorthwindExtendedEntities dal = new NorthwindExtendedEntities();
            return dal.Customers.ToList<Customer>();
        }


    }
}
