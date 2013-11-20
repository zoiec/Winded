using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NorthWindSystem.BLL;

public partial class HumanResources_EditSalesAreas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateRegions();
        }
    }
    private void PopulateRegions()
    {
        try
        {
            var controller = new NorthwindManager();
            var data = controller.GetRegions();
            RegionListView.DataSource = data;
            RegionListView.DataBind();
        }
        catch (Exception ex)
        {
            MessageLabel.Text = ex.Message;
        }
    }
}