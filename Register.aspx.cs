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

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        localhost.Service serviceObj = new localhost.Service();
        TextBox txt = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("LastName");
        int orgID = serviceObj.AccountRegistration(CreateUserWizard1.UserName, txt.Text, CreateUserWizard1.Email, CreateUserWizard1.Password);
        if (orgID > 0)
        {
            Session["OrgID"] = orgID;
            Session["username"] = CreateUserWizard1.Email;
            Response.Redirect("Home.aspx");
        }
    }
   
    protected void ContinueButton_Click1(object sender, EventArgs e)
    {
        
    }
}