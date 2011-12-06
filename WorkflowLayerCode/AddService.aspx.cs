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
using System.Configuration;


public partial class AddService : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            if (CreateServices(TextBox1.Text) > 0)
            {
                TextBox1.Text = " Added WSDL Successfully";
            }
            else
            {
                TextBox1.Text = " Error Ocurred while trying to import services";
            }
        }
    }

    #region Dynamic WSDL Info
    private int CreateServices(string wsdl_text)
    {
        Type service;
        try
        {
            //Try Database Connection
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SAASDB"].ToString());
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                //Return Error
                return -1;
            }

            localhost.Service serviceObj = new localhost.Service();

            //Insert into Data Table the services
            int count = 0;
            List<string> fieldnames = new List<string>();
            List<localhost.Field> fieldlist = new List<localhost.Field>();
            localhost.Field[] fields = serviceObj.ReadField(11, 30);
            fieldlist = new List<localhost.Field>(fields);
            foreach (localhost.Field item in fieldlist)
            {
                fieldnames.Add(count.ToString());
                count++;
            }

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

                string input_params = "";
                string return_type = "";

                MethodInfo[] methodInfo = service.GetMethods();

                foreach (MethodInfo t in methodInfo)
                {
                    List<string> valueNames = new List<string>();
                    if (t.Name == "Discover")
                        break;

                    input_params = "";
                    return_type = "";

                    foreach (ParameterInfo parameter in t.GetParameters())
                    {
                        //paramname.Text = "(" + temp.ParameterType.Name + "  " + temp.Name + ")"; 
                        input_params = input_params + parameter.Name + ":" + parameter.ParameterType.Name + " ";
                    }

                    //Get The Return type of the Service(Method)
                    return_type = t.ReturnType.ToString();

                    //Insert into Service Object(Methods Table)
                    valueNames.Add(Session["OrgID"].ToString());
                    valueNames.Add(wsdl_text);
                    valueNames.Add(t.Name);
                    valueNames.Add(input_params);
                    valueNames.Add(return_type);
                    bool success = serviceObj.InsertData(11, 30, "WSDL-Instance", fieldnames.ToArray(), valueNames.ToArray());
                    if (success == false)
                    {
                        return -1;
                    }
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }
        catch (Exception ex)
        {
            return -1;
        }

    }
    #endregion

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}