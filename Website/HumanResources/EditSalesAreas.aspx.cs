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
                    // TODO: Handle insert
                    TextBox regionBox = dataItem.FindControl("RegionDescription") as TextBox;
                    TextBox territoriesBox = dataItem.FindControl("TerritoryDescriptions") as TextBox;
                    Region region = new Region()
                    {
                        RegionDescription = regionBox.Text.Trim(),
                        Territories = new List<Territory>()
                    };
                    var territories = territoriesBox.Text.Split(',');
                    foreach (var territory in territories)
                    {
                        region.Territories.Add(new Territory() { TerritoryDescription = territory.Trim() });
                    }
                    int regionId = controller.Add(region);
                    MessageLabel.Text = "New region added (id: " + regionId + ").";
                    break;
                case "Update":
                    // TODO: Handle update
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