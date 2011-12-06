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

public partial class InsertData : System.Web.UI.Page
{
    private List<TextBox> textboxes;
    private List<Label> labels;
    public static List<localhost.Table> listOfTables;
    public List<localhost.Field> fieldlist;

    void _Default_PreRender(object sender, EventArgs e)
    {
        //remember how many textboxes we had
        ViewState["tbCount"] = textboxes.Count;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PreRender += new EventHandler(_Default_PreRender);

        if (!Page.IsPostBack)
        {   
            int OrgID = (int)Session["orgID"];

            Label1.Visible = false;
            Label2.Visible = false;

            textboxes = new List<TextBox>();
            labels = new List<Label>();

            localhost.Service serviceObj = new localhost.Service();
            localhost.Table[] array = serviceObj.GetTables(OrgID);
            listOfTables = new List<localhost.Table>(array);
            ddl.DataSource = listOfTables;
            ddl.DataTextField = "TNameProperty";
            ddl.DataValueField = "ObjIDProperty";
            ddl.DataBind();

            List<localhost.Field> fieldlist = new List<localhost.Field>();
            if (ddl.SelectedIndex >= 0)
            {
                localhost.Table tableObj = listOfTables[ddl.SelectedIndex];
                localhost.Field[] fields = serviceObj.ReadField((int)Session["orgID"], tableObj.ObjIDProperty);
                fieldlist = new List<localhost.Field>(fields);
                int count = fieldlist.Count;
                Table methodtable = new Table();
                methodtable.Width = 500;
                methodtable.BorderStyle = BorderStyle.Solid;
                methodtable.BorderWidth = 5;
                methodtable.Caption = "Object Fields";
                methodtable.HorizontalAlign = HorizontalAlign.Center;
                for (int i = 0; i < count; i++)
                {
                    TableRow tr = new TableRow();

                    TableCell tc = new TableCell();
                    Label lb = new Label();
                    lb.ID = "lb" + i;
                    lb.EnableViewState = true;
                    lb.Text = fieldlist[i].FieldNameProperty + "::";
                    labels.Add(lb);
                    tc.Controls.Add(lb);

                    TableCell tc2 = new TableCell();
                    TextBox tb = new TextBox();
                    tb.ID = "tb" + i;
                    tb.EnableViewState = true;
                    tb.Text = Request.Form[tb.ClientID];
                    textboxes.Add(tb);
                    tc2.Controls.Add(tb);

                    tr.Controls.Add(tc);
                    tr.Controls.Add(tc2);

                    methodtable.Controls.Add(tr);
                   
                }
                Panel1.Controls.Add(methodtable);
                Session["fieldlist"] = fieldlist;
            }
        }
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl.SelectedIndex >= 0)
        {
            int index_sel = ddl.SelectedIndex;
            int OrgID = (int)Session["orgID"];
            Label1.Visible = false;
            Label2.Visible = false;
            textboxes = new List<TextBox>();
            labels = new List<Label>();
            localhost.Service serviceObj = new localhost.Service();
            localhost.Table[] array = serviceObj.GetTables(OrgID);
            listOfTables = new List<localhost.Table>(array);
            ddl.DataSource = listOfTables;
            ddl.DataTextField = "TNameProperty";
            ddl.DataValueField = "ObjIDProperty";
            ddl.SelectedIndex = index_sel;
            ddl.DataBind();

            localhost.Table tableObj = listOfTables[index_sel];
            localhost.Field[] fields = serviceObj.ReadField((int)Session["orgID"], tableObj.ObjIDProperty);
            fieldlist = new List<localhost.Field>(fields);
            int count = fieldlist.Count;
            Table methodtable = new Table();
            methodtable.Width = 500;
            methodtable.BorderStyle = BorderStyle.Solid;
            methodtable.BorderWidth = 5;
            methodtable.Caption = "Object Fields";
            methodtable.HorizontalAlign = HorizontalAlign.Center;
                
            for (int i = 0; i < count; i++)
            {
                TableRow tr = new TableRow();

                TableCell tc = new TableCell();
                Label lb = new Label();
                lb.ID = "lb" + i;
                lb.EnableViewState = true;
                lb.Text = fieldlist[i].FieldNameProperty + "::";
                labels.Add(lb);
                tc.Controls.Add(lb);

                TableCell tc2 = new TableCell();
                TextBox tb = new TextBox();
                tb.ID = "tb" + i;
                tb.EnableViewState = true;
                tb.Text = Request.Form[tb.ClientID];
                textboxes.Add(tb);
                tc2.Controls.Add(tb);

                tr.Controls.Add(tc);
                tr.Controls.Add(tc2);

                methodtable.Controls.Add(tr);

            }
            Panel1.Controls.Add(methodtable);
            Session["fieldlist"] = fieldlist;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ddl.SelectedIndex >= 0)
        {
            int OrgID = (int)Session["orgID"];
            int index_sel = ddl.SelectedIndex;
            List<string> fieldnames = new List<string>();
            List<string> valueNames = new List<string>();
            Label1.Visible = false;
            Label2.Visible = false;

            localhost.Service serviceObj = new localhost.Service();
            localhost.Table tableObj = listOfTables[index_sel];

            textboxes = new List<TextBox>();
            labels = new List<Label>(); 
            fieldlist = (List<localhost.Field>)Session["fieldlist"];
            int count = fieldlist.Count;
            Table methodtable = new Table();
            methodtable.Width = 500;
            methodtable.BorderStyle = BorderStyle.Solid;
            methodtable.BorderWidth = 5;
            methodtable.Caption = "Object Fields";
            methodtable.HorizontalAlign = HorizontalAlign.Center;
                
            for (int i = 0; i < count; i++)
            {
                TableRow tr = new TableRow();

                TableCell tc = new TableCell();
                Label lb = new Label();
                lb.ID = "lb" + i;
                lb.EnableViewState = true;
                lb.Text = fieldlist[i].FieldNameProperty + "::";
                labels.Add(lb);
                tc.Controls.Add(lb);

                TableCell tc2 = new TableCell();
                TextBox tb = new TextBox();
                tb.ID = "tb" + i;
                tb.EnableViewState = true;
                tb.Text = Request.Form[tb.ClientID];
                textboxes.Add(tb);
                tc2.Controls.Add(tb);

                tr.Controls.Add(tc);
                tr.Controls.Add(tc2);

                methodtable.Controls.Add(tr);

            }
            Panel1.Controls.Add(methodtable);

            if (fieldlist != null)
            {
                count = 0;
                foreach (TextBox item in textboxes)
                {
                    valueNames.Add(item.Text);
                }

                foreach (localhost.Field item in fieldlist)
                {
                    fieldnames.Add(count.ToString());
                    count++;
                }

                bool success = serviceObj.InsertData((int)Session["orgID"], tableObj.ObjIDProperty, "", fieldnames.ToArray(), valueNames.ToArray());
                if (success)
                    Label1.Visible = true;
                else
                    Label2.Visible = true;
            }
        }

    }
}