using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NorthwindSystem.DataModels.HumanResources;
using NorthWindSystem.BLL;
using System.Data.Entity.Validation;

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

    protected void RegionListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ListViewItem dataItem = e.Item as ListViewItem;
        var controller = new NorthwindManager();

        try
        {
            string regionDescription = String.Empty;
            Region region = null;
            switch (e.CommandName)
            {
                case "Cancel":
                    RegionListView.EditIndex = -1;
                    RegionListView.InsertItemPosition = InsertItemPosition.FirstItem;
                    break;
                case "Delete":
                    break;
                case "Select":
                    break;
                case "Edit":
                    RegionListView.EditIndex = dataItem.DataItemIndex;
                    RegionListView.InsertItemPosition = InsertItemPosition.None;
                    break;
                case "Insert":
                    #region Insert Region and Territories
                    regionDescription = (dataItem.FindControl("RegionDescription") as TextBox).Text.Trim();
                    string territoryDescriptions = (dataItem.FindControl("TerritoryDescriptions") as TextBox).Text;
                    region = new Region() { RegionDescription = regionDescription, Territories = new List<Territory>() };
                    var territories = territoryDescriptions.Split(',');
                    foreach (var territory in territories)
                    {
                        region.Territories.Add(new Territory() { TerritoryDescription = territory.Trim() });
                    }
                    int regionId = controller.Add(region);
                    MessageLabel.Text = "New region added (id: " + regionId + ").";
                    #endregion

                    RegionListView.EditIndex = -1;
                    break;
                case "Update":
                    region = new Region()
                    {
                        RegionID = int.Parse((dataItem.FindControl("RegionID") as HiddenField).Value),
                        RegionDescription = (dataItem.FindControl("RegionDescription") as TextBox).Text.Trim(),
                        Territories = new List<Territory>()
                    };
                    Repeater territoryRepeater = dataItem.FindControl("TerritoryRepeater") as Repeater;
                    foreach (RepeaterItem item in territoryRepeater.Items)
                    {
                        region.Territories.Add(new Territory()
                        {
                            TerritoryID = (item.FindControl("TerritoryID") as HiddenField).Value,
                            TerritoryDescription = (item.FindControl("TerritoryDescription") as TextBox).Text.Trim()
                        });
                    }
                    controller.Update(region);

                    RegionListView.EditIndex = -1;
                    RegionListView.InsertItemPosition = InsertItemPosition.FirstItem;
                    break;
                default:
                    break;
            }
        }
        catch (DbEntityValidationException ex)
        {
            MessageLabel.Text = "The following validation errors occured:<blockquote>";
            foreach (var validationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    MessageLabel.Text += string.Format("<div>Property: {0} Error: {1}</div>", validationError.PropertyName, validationError.ErrorMessage);
                }
            }
            MessageLabel.Text += "</blockquote>";
        }
        catch (Exception ex)
        {
            MessageLabel.Text = ex.InnermostMessage();
        }

        PopulateRegions();
        e.Handled = true;
    }
}