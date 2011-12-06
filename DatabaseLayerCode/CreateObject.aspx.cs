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

public partial class FinalTrial : System.Web.UI.Page
{
    List<ListOfItems> list; //= new List<ListOfItems>();

    protected void Page_Load(object sender, EventArgs e)
    
    {
        PreRender += new EventHandler(FinalTrial_PreRender);
        Label1.Visible = false;
        if (!IsPostBack)
        {
            list = new List<ListOfItems>();
            List<DataType> Data = new List<DataType>();
            Data.Add(new DataType(0,"int"));
            Data.Add(new DataType(1,"float"));
            Data.Add(new DataType(2,"nvarchar(max)"));
            list.Add(new ListOfItems("Enter Field Name Here",""));
            GridView1.DataSource = list;
            GridView1.DataBind();
            Session["listOfItems"] = list;
            //ViewState.Add("listofItems",list.ToArray());
        }
        
        
    }

    void FinalTrial_PreRender(object sender, EventArgs e)
    {
        //list = (List<ListOfItems>)Session["listOfItems"];
        
        //foreach (TableRow item in GridView1.Rows)
        //{
        //    TextBox txt = (TextBox)item.Cells[0].FindControl("TextBox1");
        //    TextBox txtdata = (TextBox)item.Cells[1].FindControl("txtdata");
        //}
        list = (List<ListOfItems>)GridView1.DataSource;
        Session["listOfItems"] = list;
        //GridView1.DataSource = list;
        //GridView1.DataBind();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
       

    }
    protected void AddButton_Click(object sender, EventArgs e)
    {
        List<ListOfItems> newlist = new List<ListOfItems>();
       
        foreach (TableRow item in GridView1.Rows)
        {
            TextBox tb = (TextBox)item.Cells[0].FindControl("TextBox1");
            string name = tb.Text;
            TextBox txtdata = (TextBox)item.Cells[1].FindControl("txtdata");
            newlist.Add(new ListOfItems(name, txtdata.Text));
        }
        list = newlist;
        list.Add(new ListOfItems("Enter Field Name Here", ""));
       
        Session["listOfItems"] = list;
            
            GridView1.DataSource = list;
            GridView1.DataBind();
         
    }
    protected void CreateTable_Click(object sender, EventArgs e)
    {
        List<string> FieldNames = new List<string>();
        List<string> DataTypes = new List<string>();
        list = (List<ListOfItems>)GridView1.DataSource;
        //GridView1.DataSource = list;
        //GridView1.DataBind();
        
        foreach (TableRow item in GridView1.Rows)
        {

            TextBox txtName = (TextBox)item.Cells[0].FindControl("TextBox1");
            FieldNames.Add(txtName.Text);
            TextBox txtdata = (TextBox)item.Cells[1].FindControl("txtData");
            if (txtdata.Text == "int" || txtdata.Text == "nvarchar(MAX)" || txtdata.Text == "float")
                DataTypes.Add(txtdata.Text);
            else
            {
                Label2.Visible=true;
                return;
            }


        }
        localhost.Service serviceObj = new localhost.Service();
        int objid = -1;
        int counter = 0;
        bool flag = false;
        if( TextBox2.Text != "")
        {
        foreach (string item in FieldNames)
        {
            if(TextBox2.Text == item )
            {
                string[] arrayfields = FieldNames.ToArray();
                string[] arraydatatype = DataTypes.ToArray();
                objid = serviceObj.CreateObject((int)Session["orgID"], txt1.Text,arrayfields, arraydatatype,counter);
                flag = true;
                break;
            }
            counter++;
            
        }
        }
        if( flag == false && TextBox2.Text == "")
             objid = serviceObj.CreateObject((int)Session["orgID"], txt1.Text, FieldNames.ToArray(), DataTypes.ToArray(),-1);
           
        if (objid < 0)
            Label1.Visible = true;
        else
        {
            Session["objID"] = objid;
            Response.Redirect("AddRelationships.aspx");
        }
       
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TableRow trow = (TableRow)sender;
        TextBox tb = (TextBox)trow.Cells[0].FindControl("TextBox1");
        //foreach (TableRow item in GridView1.Rows)
        //{

            
        //}
        //List<ListOfItems> newlist = new List<ListOfItems>();
        //List<string> Data = new List<string>();
        //Data.Add("int");
        //Data.Add("float");
        //Data.Add("nvarchar(max)");
        //List<int> arrayOfPos = new List<int>();
        //foreach (TableRow item in GridView1.Rows)
        //{
        //    TextBox tb = (TextBox)item.Cells[0].FindControl("TextBox1");
        //    string name = tb.Text;
        //    CheckBox checkbox1 = (CheckBox)item.Cells[1].FindControl("chk1");
        //    newlist.Add(new ListOfItems(name, Data));
        //}
        //list = newlist;
        //list.Add(new ListOfItems("Enter Field Name Here", Data));

        //Session["listOfItems"] = list;

        //GridView1.DataSource = list;
        //GridView1.DataBind();

    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        TableRow trow = (TableRow)sender;
        TextBox tb = (TextBox)trow.Cells[0].FindControl("TextBox1");

    }
}
