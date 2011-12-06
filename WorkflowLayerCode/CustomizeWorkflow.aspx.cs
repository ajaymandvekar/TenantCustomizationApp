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
using System.Text;
using System.Web.Script.Serialization;

public partial class CustomizeWorkflow : System.Web.UI.Page
{
    Workflow defaultwrkflw = new Workflow();
    List<AvailableServices> availservice = new List<AvailableServices>();
    List<TextBox> lstipTextBoxes;
    List<TextBox> lstMethodNames;
    private DataTable dt = new DataTable();

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ShowServices();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PreRender += new EventHandler(CustomizeWorkflow_PreRender);
        lstipTextBoxes = new List<TextBox>();
        lstMethodNames = new List<TextBox>();
        if (!Page.IsPostBack)
        {
            ShowServices();
        }
         
        string Methods;
        string Inputs;
        string[] MethodIDs = null;
        string[] Input = null;

        #region DataSourceBinding

        //Get methods for the workflow ID
        localhost.Service serviceObj = new localhost.Service();
        localhost.TenantTableInfo obj = serviceObj.ReadDataWithGUID(11, 31, Convert.ToInt32(Session["WfID"]));
        string[] arr = obj.FieldNamesProperty;
        List<string> array = new List<string>(arr);
        string[] values = obj.FieldValuesProperty;
        List<string> valuearr = new List<string>(values);
        int counter = 0;
        defaultwrkflw.WfID = Convert.ToInt32(Session["WfID"]);
        for (int j = 0; j < array.Count; j++)
        {
            if (arr[j].ToString() == "wfname")
            {
                defaultwrkflw.WfName = valuearr[counter].ToString();
            }
            if (arr[j].ToString() == "wfmethods")
            {
                Methods = valuearr[counter].ToString();
                MethodIDs = Methods.Split(',');
            }
            if (arr[j].ToString() == "wfinputs")
            {
                Inputs = valuearr[counter].ToString();
                Input = Inputs.Split(',');
                defaultwrkflw.Inputs = Input;
            }
            counter++;
        }

        foreach (var item in MethodIDs)
        {
            defaultwrkflw.Methods.Add(Convert.ToInt32(item));
            obj = serviceObj.ReadDataWithGUID(11, 30, Int32.Parse(item));
            arr = obj.FieldNamesProperty;
            array = new List<string>(arr);
            values = obj.FieldValuesProperty;
            valuearr = new List<string>(values);
            counter = 0;
            for (int j = 0; j < array.Count; j++)
            {
                if (arr[j].ToString() == "minput")
                {
                    defaultwrkflw.InputParam.Add(valuearr[counter].ToString());
                }
                if (arr[j].ToString() == "mname")
                {
                    defaultwrkflw.MethodName.Add(valuearr[counter].ToString());
                }
                if (arr[j].ToString() == "moutput")
                {
                    defaultwrkflw.OutputParam.Add(valuearr[counter].ToString());
                }
                counter++;
            }
        }

        obj = serviceObj.ReadData(11, 30);
        arr = obj.FieldNamesProperty;
        array = new List<string>(arr);
        values = obj.FieldValuesProperty;
        valuearr = new List<string>(values);
        int countRow = valuearr.Count / array.Count;
        counter = 0;
        int addrow = 0;
        for (int i = 0; i < countRow; i++)
        {
            AvailableServices serv = new AvailableServices();
            addrow = 0;
            for (int j = 0; j < array.Count; j++)
            {
                if (arr[j].ToString() == "GUID")
                {
                    serv.MethodId = Convert.ToInt32(valuearr[counter].ToString());
                }
                if (arr[j].ToString() == "mname")
                {
                    serv.MethodName = valuearr[counter].ToString();
                }
                if (arr[j].ToString() == "minput")
                {
                    serv.InputParam = valuearr[counter].ToString();
                }
                if (arr[j].ToString() == "moutput")
                {
                    serv.OutputParam = valuearr[counter].ToString();
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
            if (addrow > 0)
            {
                availservice.Add(serv);
            }
        }
        #endregion

        #region AddControls
        int methodcounter = 0;
        Panel2.Controls.Add(new LiteralControl("<br />"));

        Label work_label = new Label();
        work_label.Text = defaultwrkflw.WfName + " Workflow Steps";
        work_label.Font.Bold = true;
        work_label.Font.Size = 20;
        Panel2.Controls.Add(work_label);
        Panel2.Controls.Add(new LiteralControl("<br />"));
        Table methodtable = new Table();
        methodtable.BorderStyle = BorderStyle.Solid;
        methodtable.BorderWidth = 5;
        methodtable.Font.Bold = true;
        methodtable.Font.Size = 10;
        methodtable.Attributes.Add("runat", "Server");
        methodtable.ID = defaultwrkflw.Methods[methodcounter].ToString();

        TableRow row = new TableHeaderRow();
        var cell1 = new TableCell();
        cell1.Text = "Service Name";
        row.Cells.Add(cell1);

        cell1 = new TableCell();
        cell1.Text = "Ouput Mapping";
        row.Cells.Add(cell1);
        methodtable.Rows.Add(row);
            
        counter = 0;
        foreach (string item in defaultwrkflw.MethodName)
        {
            if (!item.Equals(""))
            {
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                TableCell tc2 = new TableCell();
                   
                TextBox txtMethodName = new TextBox();
                txtMethodName.Width = 350;
                txtMethodName.Text = item;
                TextBox txtInputParam = new TextBox();
                txtInputParam.Text = defaultwrkflw.Inputs[counter++];
                txtInputParam.Width = 50;
                lstipTextBoxes.Add(txtInputParam);
                lstMethodNames.Add(txtMethodName);
                tc.Controls.Add(txtMethodName);
                tc2.Controls.Add(txtInputParam);
                    
                methodtable.Controls.Add(tr);
                tr.Controls.Add(tc);
                tr.Controls.Add(tc2);
            }
        }
        int methodcount = 10 - defaultwrkflw.MethodName.Count;
        while (methodcount > 0 )
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            TableCell tc2 = new TableCell();

            TextBox txtMethodName = new TextBox();
            txtMethodName.Width = 350;
            txtMethodName.Text = "";

            TextBox txtInputParam = new TextBox();
            txtInputParam.Text = "";
            txtInputParam.Width = 50;
            lstipTextBoxes.Add(txtInputParam);
            lstMethodNames.Add(txtMethodName);
            tc.Controls.Add(txtMethodName);
            tc2.Controls.Add(txtInputParam);

            methodtable.Controls.Add(tr);
            tr.Controls.Add(tc);
            tr.Controls.Add(tc2);
            methodcount--;
        }
        Panel2.Controls.Add(methodtable);
            
        #endregion
    }

    void ShowServices()
    {
        localhost.Service serviceObj = new localhost.Service();
        localhost.TenantTableInfo obj = serviceObj.ReadData(11, 30);
        string[] arr = obj.FieldNamesProperty;
        List<string> array = new List<string>(arr);
        string[] values = obj.FieldValuesProperty;
        List<string> valuearr = new List<string>(values);
        int counter = 0;
        int addrow = 0;
        int countRow = valuearr.Count / array.Count;

        
        DataColumn col1 = new DataColumn("Service Name");
        DataColumn col2 = new DataColumn("Input params");
        DataColumn col3 = new DataColumn("Output params");
        col1.DataType = System.Type.GetType("System.String");
        col2.DataType = System.Type.GetType("System.String");
        col3.DataType = System.Type.GetType("System.String");
        dt.Columns.Add(col1);
        dt.Columns.Add(col2);
        dt.Columns.Add(col3);

        for (int i = 0; i < countRow; i++)
        {
            addrow = 0;
            DataRow row = dt.NewRow();
            for (int j = 0; j < array.Count; j++)
            {
                
                if (arr[j].ToString() == "mname")
                {
                    row[col1] = valuearr[counter].ToString();
                }
                if (arr[j].ToString() == "minput")
                {
                    row[col2] =  valuearr[counter].ToString();
                }
                if (arr[j].ToString() == "moutput")
                {
                    row[col3] =  valuearr[counter].ToString();
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
            if (addrow > 0)
            {
                dt.Rows.Add(row);
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind(); 
    }

    void CustomizeWorkflow_PreRender(object sender, EventArgs e)
    {
        ViewState["tbipcount"] = lstipTextBoxes.Count;
        ViewState["tbmethodcount"] = lstMethodNames.Count;
    }

    protected void SaveWrkflw_Click(object sender, EventArgs e)
    {
        Workflow custwrkflw = new Workflow();
        int count = 0;
        string[] input = null;
        List<string> MethodIDs = new List<string>();

        foreach (TextBox item in lstMethodNames)
        {
            if (!item.Text.Equals(""))
            {
                AvailableServices serv = FindMethod(item.Text);
                custwrkflw.MethodName.Add(serv.MethodName);
                custwrkflw.Methods.Add(serv.MethodId);
                MethodIDs.Add(serv.MethodId.ToString());
            }
            TextBox ip = lstipTextBoxes[count++];
            if (!ip.Text.Equals("") && !item.Text.Equals(""))
            {
                custwrkflw.InputParam.Add(ip.Text);
            }
            if(custwrkflw.InputParam.Count > 0 )
                input = custwrkflw.InputParam.ToArray();
        }
        string[] methodNames = MethodIDs.ToArray();
        string WrkflwMethods = "";
        if (methodNames != null)
        {
            WrkflwMethods = string.Join(",",methodNames);
        }
        custwrkflw.WrkflwMethods = WrkflwMethods;
                
        
        if( input != null)
            custwrkflw.WrkflwInputs = string.Join(",",input);
        SaveToDB(custwrkflw);
    }

    private void SaveToDB(Workflow customwrkflw)
    {
        customwrkflw.WfName = txtWorkflowName.Text;

        List<String> fieldnames = new List<String>();
        fieldnames.Add("0");
        fieldnames.Add("1");
        fieldnames.Add("2");
        fieldnames.Add("3");
        List<String> valueNames = new List<String>();
        valueNames.Add(Session["OrgID"].ToString());
        valueNames.Add(customwrkflw.WfName);
        valueNames.Add(customwrkflw.WrkflwMethods);
        valueNames.Add(customwrkflw.WrkflwInputs);

        localhost.Service serviceObj = new localhost.Service();
        bool success = serviceObj.InsertData(11, 31, "Worfkflow-Instance", fieldnames.ToArray(), valueNames.ToArray());
        if (success)
        {
            Label1.Text = "Worflow added successfully";
            Label1.Visible = true;
        }
        else
        {
            Label1.Text = "Operation Unsuccessfull";
            Label1.Visible = true;
        }
    }

    public AvailableServices FindMethod(string MethodName)
    {
        AvailableServices serv = new AvailableServices();
        foreach (AvailableServices item in availservice)
        {
            if (item.MethodName.Equals(MethodName))
            {
                serv = item;
                break;
            }
        }
        return serv;
    }
}