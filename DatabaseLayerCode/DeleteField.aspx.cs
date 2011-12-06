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

public partial class DeleteField : System.Web.UI.Page
{
    List<localhost.Table> listOfTables;
    List<localhost.Field> values;
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
            ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
            ddl.DataBind();
            Label3.Visible = true;
            Label4.Visible = true;
            ViewState["listOfTables"] = listOfTables;
        }


    }

    void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl.SelectedIndex > 0)
        {
            localhost.Table tableObj = listOfTables[ddl.SelectedIndex];

            localhost.Service serviceObj = new localhost.Service();
            localhost.Field[] array = serviceObj.ReadField((int)Session["orgID"], tableObj.ObjIDProperty);
            values = new List<localhost.Field>(array);
            DropDownList1.DataSource = values;
            DropDownList1.DataTextField = "FieldNameProperty";
            DropDownList1.DataValueField = "FieldIDProperty";
            DropDownList1.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddl.SelectedIndex >= 0 && DropDownList1.SelectedIndex >= 0)
        {
            localhost.Service serviceObj = new localhost.Service();
            values = (List<localhost.Field>)ViewState["listOfValues"];
            listOfTables = (List<localhost.Table>)ViewState["listOfTables"];
            localhost.Table tableObj = listOfTables[ddl.SelectedIndex];
            localhost.Field fieldObj = values[DropDownList1.SelectedIndex];
            bool success = serviceObj.DeleteField((int)Session["orgID"], tableObj.ObjIDProperty, fieldObj.FieldIDProperty);
            if (success)
                Label1.Visible = true;
            else
                Label2.Visible = true;

            int OrgID = (int)Session["orgID"];

            localhost.Table[] array = serviceObj.GetTables(OrgID);
            listOfTables = new List<localhost.Table>(array);
            ddl.DataSource = listOfTables;
            ddl.DataTextField = "TNameProperty";
            ddl.DataValueField = "ObjIDProperty";
            ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
            ddl.DataBind();
            ViewState["listOfTables"] = listOfTables;
            values = null;
            DropDownList1.DataSource = null;
            DropDownList1.DataBind();
        }
    }
    protected void ShwField_Click(object sender, EventArgs e)
    {
        listOfTables = (List<localhost.Table>)ViewState["listOfTables"];
        localhost.Table tableObj = listOfTables[ddl.SelectedIndex];

        localhost.Service serviceObj = new localhost.Service();
        localhost.Field[] array = serviceObj.ReadField((int)Session["orgID"], tableObj.ObjIDProperty);
        values = new List<localhost.Field>(array);
        DropDownList1.DataSource = values;
        DropDownList1.DataTextField = "FieldNameProperty";
        DropDownList1.DataValueField = "FieldIDProperty";
        DropDownList1.DataBind();
        ViewState["listOfValues"] = values;

    }
}