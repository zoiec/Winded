using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NorthWindSystem.BLL;
using Sales = NorthwindSystem.DataModels.Sales;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string AsBase64(object photo)
    {
        string img = "data:image/jpg;base64,{0}";
        byte[] bytes = photo as byte[];
        if (bytes != null)
        {
            using (var memStream = new MemoryStream())
            {
                int offset = 78; // For Northwind images only - legacy of the OLE image format
                memStream.Write(bytes, offset, bytes.Length - offset);
                img = string.Format(img, Convert.ToBase64String(memStream.ToArray()));
            }
        }
        else
        {
            img = "";
        }
        return img;
    }

    private List<Sales.Order> _Orders = null;
    private List<Sales.Order> Orders
    {
        get
        {
            if (_Orders == null)
            {
                var controller = new NorthwindManager();
                _Orders = controller.GetOrders();
            }
            return _Orders;
        }
    }
    public string EmployeePerformance(object employeeId)
    {
        // Alternatively, push this logic to the BLL
        // return new NorthwindManager().EmployeePerformance(employeeId as int?);
        string review;
        int? empId = employeeId as int?;
        var shortList = Orders.Where(item => item.EmployeeID == empId).ToList();
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