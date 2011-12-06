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

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["objID"] = null;

    }
    protected void BtnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateObject.aspx");
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeleteObject.aspx");
    }
    protected void BtnInsert_Click(object sender, EventArgs e)
    {
        Response.Redirect("InsertData.aspx");
    }
    protected void BtnModify_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeleteField.aspx");
    }
    protected void BtnViewObj_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewObject.aspx");
    }
    protected void BtnViewData_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewData.aspx");
    }
    protected void BtnRelationship_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRelationship.aspx");
    }
}