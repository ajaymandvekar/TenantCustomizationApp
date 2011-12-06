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

public partial class DeleteTable : System.Web.UI.Page
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
            Label3.Visible = false;
            Label4.Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddl.SelectedIndex >= 0 )
        {
            localhost.Service serviceObj = new localhost.Service();
            
            bool success = serviceObj.DeleteTable((int)Session["orgID"], Convert.ToInt32(ddl.SelectedValue));
            if (success)
                Label3.Visible = true;
            else
                Label4.Visible = false;

            int OrgID = (int)Session["orgID"];
            localhost.Table[] array = serviceObj.GetTables(OrgID);
            listOfTables = new List<localhost.Table>(array);
            ddl.DataSource = listOfTables;
            ddl.DataTextField = "TNameProperty";
            ddl.DataValueField = "ObjIDProperty";
            ddl.DataBind();
           
        }

     
    }
}