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
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services;
using System.Web.Services.Description;
using System.Xml.Serialization;
using System.Reflection;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;


public partial class _Default : System.Web.UI.Page
{
    Type service;
    string[] method_array = null;
    string[] mapping_array = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        int wfid = Convert.ToInt32(Session["WfID"]);
        if (!IsPostBack)
        {
            Session["next_index"] = 0;
            InvokeWorkFlow(wfid);
        }
        else
        {
            InvokeWorkFlow(wfid);
        }

    }


    #region Dynamic method Invocation
    private string InvokeMethod(string wsdl_text,string MethodName,Object[] param1)
    {
        try
        {
            Uri uri = new Uri(wsdl_text);
            WebRequest webRequest = WebRequest.Create(uri);
            System.IO.Stream requestStream = webRequest.GetResponse().GetResponseStream();

            // Get a WSDL file describing a service
            ServiceDescription sd = ServiceDescription.Read(requestStream);
            string sdName = sd.Services[0].Name;

            // Initialize a service description servImport
            ServiceDescriptionImporter servImport = new ServiceDescriptionImporter();
            servImport.AddServiceDescription(sd, String.Empty, String.Empty);
            servImport.ProtocolName = "Soap";
            servImport.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

            CodeNamespace nameSpace = new CodeNamespace();
            CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
            codeCompileUnit.Namespaces.Add(nameSpace);
            // Set Warnings
            ServiceDescriptionImportWarnings warnings = servImport.Import(nameSpace, codeCompileUnit);

            if (warnings == 0)
            {
                StringWriter stringWriter = new StringWriter(System.Globalization.CultureInfo.CurrentCulture);
                Microsoft.CSharp.CSharpCodeProvider prov = new Microsoft.CSharp.CSharpCodeProvider();
                prov.GenerateCodeFromNamespace(nameSpace, stringWriter, new CodeGeneratorOptions());

                // Compile the assembly with the appropriate references
                string[] assemblyReferences = new string[2] { "System.Web.Services.dll", "System.Xml.dll" };
                CompilerParameters param = new CompilerParameters(assemblyReferences);
                param.GenerateExecutable = false;
                param.GenerateInMemory = true;
                param.TreatWarningsAsErrors = false;
                param.WarningLevel = 4;

                CompilerResults results = new CompilerResults(new TempFileCollection());
                results = prov.CompileAssemblyFromDom(param, codeCompileUnit);
                Assembly assembly = results.CompiledAssembly;
                service = assembly.GetType(sdName);

                MethodInfo[] methodinfo = service.GetMethods();
                string result = null;

                foreach (MethodInfo t in methodinfo)
                if (t.Name == MethodName)
                {
                    //Invoke Method
                    Object obj = Activator.CreateInstance(service);
                    Object response = t.Invoke(obj, param1);
                    

                    Array myArrayList = response as Array;
                    if (myArrayList != null)
                    {
                        List<Object> result_obj = new List<Object>();
                  
                        foreach (var item in myArrayList)
                        {
                            foreach (var currentPropertyInformation in item.GetType().GetProperties())
                            {
                                //currentPropertyInformation.GetValue(item, null);
                                //Result.Text = Result.Text + currentPropertyInformation.Name + ":" + currentPropertyInformation.GetValue(item, null);
                                result = currentPropertyInformation.GetValue(item, null).ToString();
                            }
                        }
                    }
                    else if(response.GetType().ToString() != "System.String") 
                    {
                        foreach (var currentPropertyInformation in response.GetType().GetProperties())
                        {
                            //currentPropertyInformation.GetValue(item, null);
                            //Result.Text = Result.Text + currentPropertyInformation.Name + ":" + currentPropertyInformation.GetValue(item, null);
                            if (currentPropertyInformation.GetValue(response, null) != null)
                            {
                                result = result + currentPropertyInformation.Name + ":" + currentPropertyInformation.GetValue(response, null) + "|";
                            }
                            else
                            {
                                result = result + currentPropertyInformation.Name + ":NULL,";
                            }
                        }
                        
                    }

                    if(response!=null && result==null)
                    {
                        result =  response.ToString();
                    }
                   
                
                    break;
                }
                    return result;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion

    #region Dynamic Workflow Invocation
    private void check_if_to_proceed(string curr_methodid)
    {
        string output = "";
        string inputparam_str = "";
        string methodname = "";
        string wsdltext = "";
        Table curr_table = form1.FindControl(curr_methodid) as Table;

        localhost.Service serviceObj = new localhost.Service();
        localhost.TenantTableInfo obj = serviceObj.ReadDataWithGUID(11, 30, Int32.Parse(curr_methodid));
        string[] arr = obj.FieldNamesProperty;
        List<string> array = new List<string>(arr);
        string[] values = obj.FieldValuesProperty;
        List<string> valuearr = new List<string>(values);
        int counter = 0;

        for (int j = 0; j < array.Count; j++)
        {
            if (arr[j].ToString() == "minput")
            {
                inputparam_str = valuearr[counter].ToString();
            }
            if (arr[j].ToString() == "mname")
            {
                methodname = valuearr[counter].ToString();
            }
            if (arr[j].ToString() == "wsdl")
            {
                wsdltext = valuearr[counter].ToString();
            }
            counter++;
        }

        if (inputparam_str != "")
        {
            string[] inputp = inputparam_str.Split(' ');
            object[] param1 = new object[inputp.Length - 1];
            for (int ip = 0; ip < inputp.Length - 1; ip++)
            {
                string[] p = inputp[ip].Split(':');
                try
                {
                    if (String.Compare(p[1].Substring(0, 6), "System.") != 0)
                    {
                        p[1] = "System." + p[1];
                    }
                }
                catch (Exception eexc)
                {
                    p[1] = "System." + p[1];
                }

                TextBox txt_box = curr_table.FindControl("arg-" + curr_methodid + "-" + ip.ToString()) as TextBox;
                param1[ip] = Convert.ChangeType(txt_box.Text, System.Type.GetType(p[1]));

            }
            output = InvokeMethod(wsdltext, methodname, param1);
        }
        else
        {
            output = InvokeMethod(wsdltext, methodname, null);
        }


        if (output != null)
        {
            int mapped_flag = 0;
            //Display result in current method result box
            TextBox resultbox = curr_table.FindControl("result-" + curr_methodid) as TextBox;
            resultbox.Text = output;

            //Check if we need to map the output to next method
            int index = Convert.ToInt32(Session["next_index"]);

            if (index < method_array.Length)
            {
                if (Convert.ToInt32(mapping_array[index]) != 0)
                {
                    int arg = Convert.ToInt32(mapping_array[index]) - 1;
                    TextBox next_param = curr_table.FindControl("arg-" + curr_table.ToolTip + "-" + arg.ToString()) as TextBox;
                    next_param.Text = output;
                    mapped_flag = 1;
                }
                Session["next_index"] = index + 1;
            }
            else
            {
                Session["next_index"] = 0;
            }

            string[] nextp = null;
            if (Int32.Parse(curr_table.ToolTip) > 0)
            {
                obj = serviceObj.ReadDataWithGUID(11, 30, Int32.Parse(curr_table.ToolTip));
                arr = obj.FieldNamesProperty;
                array = new List<string>(arr);
                values = obj.FieldValuesProperty;
                valuearr = new List<string>(values);
                counter = 0;
                for (int j = 0; j < array.Count; j++)
                {
                    if (arr[j].ToString() == "minput")
                    {
                        inputparam_str = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "mname")
                    {
                        methodname = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "wsdl")
                    {
                        wsdltext = valuearr[counter].ToString();
                    }
                    counter++;
                }
            }

            if (inputparam_str != "")
            {
                nextp = inputparam_str.Split(' ');
            }
            if (nextp != null && (nextp.Length - 1) == 1 && mapped_flag == 1)
            {
                check_if_to_proceed(curr_table.ToolTip);
            }
            else if (nextp == null && index != (method_array.Length - 1))
            {
                check_if_to_proceed(curr_table.ToolTip);
            }
        }
    }

    private void processMethodRequest(object sender, CommandEventArgs e)
    {
        string output = "";
        string inputparam_str = "";
        string methodname = "";
        string wsdltext = "";
        Table curr_table = form1.FindControl(e.CommandArgument.ToString()) as Table;

        localhost.Service serviceObj = new localhost.Service();
        localhost.TenantTableInfo obj = serviceObj.ReadDataWithGUID(11, 30, Int32.Parse(e.CommandArgument.ToString()));
        string[] arr = obj.FieldNamesProperty;
        List<string> array = new List<string>(arr);
        string[] values = obj.FieldValuesProperty;
        List<string> valuearr = new List<string>(values);
        int counter = 0;

        for (int j = 0; j < array.Count; j++)
        {
            if (arr[j].ToString() == "minput")
            {
                inputparam_str = valuearr[counter].ToString();
            }
            if (arr[j].ToString() == "mname")
            {
                methodname = valuearr[counter].ToString();
            }
            if (arr[j].ToString() == "wsdl")
            {
                wsdltext = valuearr[counter].ToString();
            }
            counter++;
        }

        if (inputparam_str != "")
        {
            string[] inputp = inputparam_str.Split(' ');
            object[] param1 = new object[inputp.Length - 1];
            for (int ip = 0; ip < inputp.Length - 1; ip++)
            {
                string[] p = inputp[ip].Split(':');
                try
                {
                    if (String.Compare(p[1].Substring(0, 6), "System.") != 0)
                    {
                        p[1] = "System." + p[1];
                    }
                }
                catch(Exception eexc)
                {   
                        p[1] = "System." + p[1]; 
                }

                TextBox txt_box = curr_table.FindControl("arg-" + e.CommandArgument.ToString() + "-" + ip.ToString()) as TextBox;     
                param1[ip] = Convert.ChangeType(txt_box.Text, System.Type.GetType(p[1]));
                        
            }
            output = InvokeMethod(wsdltext, methodname, param1);
        }
        else
        {
            output = InvokeMethod(wsdltext, methodname, null);
        }
            

        if (output != null)
        {
            int mapped_flag = 0;
            //Display result in current method result box
            TextBox resultbox = curr_table.FindControl("result-" + e.CommandArgument.ToString()) as TextBox;
            resultbox.Text = output;

            //Check if we need to map the output to next method
            int index = Convert.ToInt32(Session["next_index"]);

            if (index < method_array.Length)
            {
                if (Convert.ToInt32(mapping_array[index]) != 0)
                {
                    int arg = Convert.ToInt32(mapping_array[index]) - 1;
                    TextBox next_param = curr_table.FindControl("arg-" + curr_table.ToolTip + "-" + arg.ToString()) as TextBox;
                    next_param.Text = output;
                    mapped_flag = 1;
                }
                Session["next_index"] = index + 1;
            }
            else
            {
                Session["next_index"] = 0;
            }

            string[] nextp = null;
            if (Int32.Parse(curr_table.ToolTip) > 0)
            {
                obj = serviceObj.ReadDataWithGUID(11, 30, Int32.Parse(curr_table.ToolTip));
                arr = obj.FieldNamesProperty;
                array = new List<string>(arr);
                values = obj.FieldValuesProperty;
                valuearr = new List<string>(values);
                counter = 0;
                for (int j = 0; j < array.Count; j++)
                {
                    if (arr[j].ToString() == "minput")
                    {
                        inputparam_str = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "mname")
                    {
                        methodname = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "wsdl")
                    {
                        wsdltext = valuearr[counter].ToString();
                    }
                    counter++;
                }
            }

            if (inputparam_str != "")
            {
                nextp = inputparam_str.Split(' ');
            }
            
            if (nextp != null && (nextp.Length-1) == 1 && mapped_flag == 1)
            {
                check_if_to_proceed(curr_table.ToolTip);
            }
            else if (nextp == null && index != (method_array.Length-1))
            {
                check_if_to_proceed(curr_table.ToolTip);
            }
        }
    }

    private void InvokeWorkFlow(int workflow_id)
    {
        int id = 0;
        int nid = 0;
        String method_str = null;
        String ip_mapping_str = null;
        String workflowname = null;
        string inputparam_str = null;

        //Get methods for the workflow ID
        localhost.Service serviceObj = new localhost.Service();
        localhost.TenantTableInfo obj = serviceObj.ReadDataWithGUID(11,31,workflow_id);
        string[] arr = obj.FieldNamesProperty;
        List<string> array = new List<string>(arr);
        string[] values = obj.FieldValuesProperty;
        List<string> valuearr = new List<string>(values);
        int counter = 0;

        for (int j = 0; j < array.Count; j++)
        {
                
                if (arr[j].ToString() == "wfname")
                {
                    workflowname = valuearr[counter].ToString();
                }
                if (arr[j].ToString() == "wfmethods")
                {
                    method_str = valuearr[counter].ToString();
                    method_array = method_str.Split(',');
                }
                if (arr[j].ToString() == "wfinputs")
                {
                    ip_mapping_str = valuearr[counter].ToString();
                    mapping_array = ip_mapping_str.Split(',');
                }
                counter++;
        }
        
        if (Convert.ToInt32(Session["next_index"]) == 0)
        {
            Session["next_index"] = 1;
        }

        Panel panel1 = new Panel();
        panel1.Width = 520;
        panel1.HorizontalAlign = HorizontalAlign.Center;
        panel1.BackColor = System.Drawing.Color.Khaki;
        //panel1.BorderStyle = BorderStyle.Groove;
        Label wfname = new Label();
        wfname.ForeColor = System.Drawing.Color.Black;
        wfname.Text = workflowname;
        wfname.Font.Bold = true;
        panel1.Controls.Add(wfname);
        form1.Controls.Add(panel1);
        form1.Controls.Add(new LiteralControl("<br />"));

        if (method_array != null)
        {
            for (int iter_methods = 0; iter_methods < method_array.Length; iter_methods++)
            {
                id = Convert.ToInt32(method_array[iter_methods]);
                if (iter_methods + 1 < method_array.Length)
                    nid = Convert.ToInt32(method_array[iter_methods + 1]);
                else
                    nid = 0;

                obj = serviceObj.ReadDataWithGUID(11, 30, id);
                arr = obj.FieldNamesProperty;
                array = new List<string>(arr);
                values = obj.FieldValuesProperty;
                valuearr = new List<string>(values);
                counter = 0;
                inputparam_str = "";

                panel1 = new Panel();
                panel1.Width = 520;
                panel1.BackColor = System.Drawing.Color.Khaki;
                panel1.BorderStyle = BorderStyle.Dashed;
                panel1.HorizontalAlign = HorizontalAlign.Center;

                for (int j = 0; j < array.Count; j++)
                {

                    if (arr[j].ToString() == "mname")
                    {
                        Label methodname = new Label();
                        methodname.Font.Bold = true;
                        methodname.Text = "Webservice : " + valuearr[counter].ToString();
                        panel1.Controls.Add(methodname);
                    }
                    if (arr[j].ToString() == "minput")
                    {
                        inputparam_str = valuearr[counter].ToString();
                    }
                    counter++;
                }

                Table methodtable = new Table();
                methodtable.Width = 500;
                methodtable.Attributes.Add("runat", "Server");
                methodtable.ID = id.ToString();
                methodtable.ToolTip = nid.ToString();
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();

                methodtable.Controls.Add(tr);

                
                if (inputparam_str != "")
                {
                    int arg_count = 0;
                    string[] inputp = inputparam_str.Split(' ');
                    for (int ip = 0; ip < inputp.Length - 1; ip++)
                    {
                        string[] p = inputp[ip].Split(':');

                        tr = new TableRow();
                        tr.Font.Bold = true;
                        tr.Font.Size = 10;
                        tc = new TableCell();
                        Label paramname = new Label();
                        paramname.Text = p[0].ToString() + ":" + p[1].ToString();
                        tc.Controls.Add(paramname);
                        tr.Controls.Add(tc);

                        tc = new TableCell();
                        TextBox tb = new TextBox();
                        tb.Attributes.Add("runat", "Server");
                        tb.Attributes.Add("Tooltip", p[1].ToString());
                        tb.EnableViewState = true;
                        tb.MaxLength = 128;
                        tb.ID = "arg-" + id + "-" + arg_count;
                        tc.Controls.Add(tb);
                        tr.Controls.Add(tc);

                        arg_count++;
                        methodtable.Controls.Add(tr);
                    }
                }

                tr = new TableRow();
                tr.Font.Bold = true;
                TableCell cell = new TableCell();
                Button btn = new Button();
                btn.Text = "Invoke service";

                /*Would like to pass value of i as argument to myFunction*/
                btn.CommandArgument = id.ToString();
                btn.Command += new CommandEventHandler(processMethodRequest);
                cell.Controls.Add(btn);
                tr.Controls.Add(cell);
                tr.Font.Bold = true;
                methodtable.Controls.Add(tr);
                tr = new TableRow();
                methodtable.Controls.Add(tr);

                tr = new TableRow();
                tr.Font.Bold = true;
                tc = new TableCell();
                Label result_box = new Label();
                result_box.Text = "Result";
                tc.Controls.Add(result_box);
                tr.Controls.Add(tc);

                tc = new TableCell();
                TextBox result_str = new TextBox();
                result_str.Attributes.Add("runat", "Server");
                result_str.EnableViewState = true;
                result_str.ID = "result-" + id.ToString();
                tc.Controls.Add(result_str);
                tr.Controls.Add(tc);

                methodtable.Controls.Add(tr);

                panel1.Controls.Add(methodtable);
                form1.Controls.Add(panel1);

                
                if (iter_methods + 1 < method_array.Length)
                {
                    panel1 = new Panel();
                    panel1.Width = 520;
                    Image image = new Image();
                    image.ImageUrl = "../images/arrow_solid_down.png";
                    image.Height = 50;
                    image.Attributes.Add("Style", "padding-left:250px;");
                    panel1.Controls.Add(image);
                    form1.Controls.Add(panel1);
                }
            }
        }
    }
    #endregion



}