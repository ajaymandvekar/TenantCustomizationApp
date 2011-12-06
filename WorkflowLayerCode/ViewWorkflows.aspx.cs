/*
 * Copyright 2011 
 * Ajay Mandvekar(ajaymandvekar@gmail.com),Mugdha Kolhatkar(himugdha@gmail.com),Vishakha Channapattan(vishakha.vc@gmail.com)
 * 
 * This file is part of TenantCustomizationApp.
 * TenantCustomizationApp is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * TenantCustomizationApp is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with TenantCustomizationApp.  If not, see <http://www.gnu.org/licenses/>.
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class ViewWorkflows : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
            localhost.Service serviceObj = new localhost.Service();
            localhost.TenantTableInfo obj = serviceObj.ReadData(11, 31);
            string[] arr = obj.FieldNamesProperty;
            List<string> array = new List<string>(arr);
            string[] values = obj.FieldValuesProperty;
            List<string> valuearr = new List<string>(values);
            int countRow = valuearr.Count / array.Count;
            int counter = 0;

            TableRow row = new TableHeaderRow();
            var cell1 = new TableCell();
            row.TableSection = TableRowSection.TableHeader;
            cell1.Text = "Global ID";
            row.Cells.Add(cell1);
            WfTable.Rows.Add(row);

            row.TableSection = TableRowSection.TableHeader;
            cell1 = new TableCell();
            cell1.Text = "Workflow Name";
            row.Cells.Add(cell1);
            WfTable.Rows.Add(row);

            row.TableSection = TableRowSection.TableHeader;
            cell1 = new TableCell();
            cell1.Text = "Selection";
            row.Cells.Add(cell1);
            WfTable.Rows.Add(row);

            int addrow = 0;

            for (int i = 0; i < countRow; i++)
            {
                addrow = 0;
                row = new TableRow();
                RadioButton rbnew = new RadioButton();
                if (i == 0)
                {
                    rbnew.Checked = true;
                }
                for (int j = 0; j < array.Count; j++)
                {
                   TableCell cell = new TableCell();
                   if (arr[j].ToString() == "GUID")
                   {
                       cell.Text = valuearr[counter].ToString();
                       rbnew.ID = valuearr[counter].ToString();
                       rbnew.EnableViewState = true;
                       rbnew.CheckedChanged += new EventHandler(Changed);
                       rbnew.AutoPostBack = true;
                       rbnew.GroupName = "Check";
                       row.Cells.Add(cell);
                   }
                   if (arr[j].ToString() == "wfname")
                   {
                       cell.Text = valuearr[counter].ToString();
                       row.Cells.Add(cell);
                   }
                   if (arr[j].ToString() == "ownerid")
                   {
                       if (Convert.ToInt32(valuearr[counter]) == 11 || Convert.ToInt32(valuearr[counter]) == Convert.ToInt32(Session["OrgId"]))
                       {
                           addrow = 1;
                       }
                   }
                   counter++;
                }
            
                if (addrow == 1)
                {
                    TableCell cellcheck = null;
                    row.Cells.Add(cellcheck = new TableCell());
                    ((IParserAccessor)cellcheck).AddParsedSubObject(rbnew);
                    WfTable.Rows.Add(row);
                }
            }
    }

    protected void Changed(object sender, EventArgs e)
    {
        Session["WfID"] = Convert.ToInt32(((RadioButton)sender).ID);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["WfID"]) > 0)
        {
            Response.Redirect("CustomizeWorkflow.aspx");
        }
           
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["WfID"]) > 0)
        {
            Response.Redirect("TestWorkFlow.aspx");
        }
    }
}