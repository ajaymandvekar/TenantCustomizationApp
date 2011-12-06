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

public partial class ViewData : System.Web.UI.Page
{
    List<localhost.Table> listOfTables;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int OrgID = (int)Session["orgID"];
            localhost.Service serviceObj = new localhost.Service();
            localhost.Table[] array = serviceObj.GetTables(OrgID);
            listOfTables = new List<localhost.Table>(array);
            ddl.DataSource = listOfTables;
            ddl.DataTextField = "TNameProperty";
            ddl.DataValueField = "ObjIDProperty";
            ddl.DataBind();
        }
        else
        {
            int OrgID = (int)Session["orgID"];
            localhost.Service serviceObj = new localhost.Service();
            localhost.Table[] array = serviceObj.GetTables(OrgID);
            listOfTables = new List<localhost.Table>(array);
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        if (ddl.SelectedIndex >= 0)
        {
            localhost.Service serviceObj = new localhost.Service();
            localhost.Table tableObj = listOfTables[ddl.SelectedIndex];
            localhost.TenantTableInfo obj = serviceObj.ReadData((int)Session["orgID"],tableObj.ObjIDProperty);
            string[] arr = obj.FieldNamesProperty;
            List<string> array = new List<string>(arr);
            string[] values = obj.FieldValuesProperty;
            List<string> valuearr = new List<string>(values);
            int countRow = valuearr.Count / array.Count;
            int counter = 0;
            for (int i = 0; i < countRow; i++)
            {

                TableRow row = new TableRow();
                for (int j = 0; j < array.Count; j++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = valuearr[counter++].ToString();
                    row.Cells.Add(cell);

                }
                Table1.Rows.Add(row);
            }
        }
    }
    
    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}