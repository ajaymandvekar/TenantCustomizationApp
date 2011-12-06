<%--
Copyright 2011 Ajay Mandvekar(ajaymandvekar@gmail.com), Mugdha Kolhatkar(himugdha@gmail.com),Vishakha Channapattan(vishakha.vc@gmail.com)

This file is part of TenantCustomizationApp.

TenantCustomizationApp is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

TenantCustomizationApp is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with TenantCustomizationApp.  If not, see <http://www.gnu.org/licenses/>.
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateObject.aspx.cs" Inherits="FinalTrial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Object</title>
     <link rel="stylesheet" href="../images/HarvestField.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrap">

	<!--header -->
	<div id="header">			
				
		<h1 id="logo-text"><a href="../Home.aspx" title="">SalesForce</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LblWelcome" runat="server" Text=""></asp:Label>
        </h1>
				
				
	<!--header ends-->					
	</div>
		
	<!-- navigation starts-->	
	<div  id="nav">
		
		<div id="light-brown-line"></div>	
		
		<ul>
			<li id="current"><a href="../Home.aspx">Home</a></li>
			</ul>
		
	<!-- navigation ends-->	
	</div>	
		</div>
    <div>
    
    
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    
    &nbsp;<asp:Label ID="lb1" runat="server" Font-Bold="True" Font-Italic="True">Enter Table Name</asp:Label>
        :&nbsp;
        <asp:TextBox ID="txt1" runat="server" Width="188px"></asp:TextBox>
        <asp:GridView ID="GridView1" Runat="server" AutoGenerateColumns="False" 
    CellPadding="2" ForeColor="#333333" EnableModelValidation="True" 
            ondatabinding="GridView1_DataBinding" ondatabound="GridView1_DataBound" 
            onrowdatabound="GridView1_RowDataBound" 
            onrowediting="GridView1_RowEditing" onrowupdated="GridView1_RowUpdated" 
            Height="82px" Width="369px" style="margin-left:443px;" 
            HorizontalAlign="Center">
        <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
    <PagerStyle ForeColor="White" HorizontalAlign="Center" 
       BackColor="#284775"></PagerStyle>
    <HeaderStyle ForeColor="White" Font-Bold="True" 
       BackColor="#5D7B9D"></HeaderStyle>
    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
    <Columns>
        <asp:TemplateField HeaderText="Field Name">
            <ItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Text") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Data Type">
            <ItemTemplate>
                <asp:TextBox ID="txtData" runat="server" Text='<%# Eval("TypeName") %>'></asp:TextBox>           
            </ItemTemplate>
        </asp:TemplateField>
                
    </Columns>
    <SelectedRowStyle ForeColor="#333333" Font-Bold="True" 
         BackColor="#E2DED6"></SelectedRowStyle>
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

</asp:GridView>

        
        <asp:Button ID="AddButton" runat="server" onclick="AddButton_Click" 
            Text="Add Row" class="button" Width="80px" />
          
        <br />
        <br />
        <br />
        <asp:Label ID="Lbl2" runat="server" Font-Bold="True" Font-Italic="True">Select Primary Key</asp:Label>
       
        :
        <asp:TextBox ID="TextBox2" runat="server" Height="21px"></asp:TextBox>
       
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
        &nbsp;<br />
        <asp:Button ID="CreateTable" Text="Create Table" runat="server" onclick="CreateTable_Click" class="button"
             />
              <br />
              <p>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="Label2" runat="server" Visible="false" ForeColor="Red">Please enter valid datatype values(int,nvarchar(MAX),float)</asp:Label>
         </p>
             <p>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Label ID="Label1" runat="server" Visible="false" ForeColor="Red">Error while creating table</asp:Label>
         </p>
    </div>
    </form>
</body>
</html>
