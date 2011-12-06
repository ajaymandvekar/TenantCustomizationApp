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

public partial class AddRelationships : System.Web.UI.Page
{
    List<localhost.Field> FieldList;
    List<localhost.Table> lstOfTables;
    protected void Page_Load(object sender, EventArgs e)
    {
        localhost.Service serviceObj = new localhost.Service();
        int orgID = (int)Session["orgID"];
        int objID = (int)Session["objID"];
        localhost.Field[] array = serviceObj.ReadField(orgID, objID);
        FieldList = new List<localhost.Field>(array);
        DropDownList1.DataSource = FieldList;
        DropDownList1.DataTextField = "FieldNameProperty";
        DropDownList1.DataValueField = "FieldIDProperty";
        DropDownList1.DataBind();
        
        Label4.Visible = false;
        Label3.Visible = false;
    }

    

    protected void BtnForeignKey_Click(object sender, EventArgs e)
    {
        
        if (DropDownList1.SelectedIndex >= 0)
        {
            int index = DropDownList1.SelectedIndex;
            localhost.Field selectedField = FieldList[index];
            localhost.Service serviceObj = new localhost.Service();
            int orgID = (int)Session["orgID"];
            int objID = (int)Session["objID"];
            localhost.Table[] tablelst = serviceObj.getTablesWithPrimaryKey(orgID, objID, selectedField.FieldDataType);
            lstOfTables = new List<localhost.Table>(tablelst);
            DropDownList2.DataSource = lstOfTables;
            DropDownList2.DataTextField = "TNameProperty";
            DropDownList2.DataValueField = "ObjIDProperty";
            DropDownList2.DataBind();
            ViewState["foreignkey"] = lstOfTables;

        }
        //serviceObj.getTablesWithPrimaryKey((int)Session["orgID"],
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex >= 0 && DropDownList1.SelectedIndex >= 0)
        {


                lstOfTables = (List<localhost.Table>)ViewState["foreignkey"];
                localhost.Table tableObj = lstOfTables[DropDownList2.SelectedIndex];
                int index = DropDownList1.SelectedIndex;
                localhost.Field selectedField = FieldList[index];
                localhost.Service serviceObj = new localhost.Service();
                int orgID = (int)Session["orgID"];
                int ObjID = (int)Session["objID"];
                bool success = serviceObj.InsertRelationship(orgID, ObjID, tableObj.ObjIDProperty, selectedField.FieldIDProperty);
                if (success)
                    Label3.Visible = true;
                else
                    Label4.Visible = true;
            
        }
    }
}