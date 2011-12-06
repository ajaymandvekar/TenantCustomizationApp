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
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomizeWorkflow.aspx.cs" Inherits="CustomizeWorkflow" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../images/HarvestField.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="wrap">

	<!--header -->
	<div id="header">			
				
		<h1 id="logo-text"><a href="../Home.aspx" title="">SalesForce</a>
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
		
	<!-- content-wrap starts -->
	<div id="content-wrap">
    <div id="main">
        <asp:Panel ID="Panel2" runat="server" />
     <asp:Panel ID="Panel3" runat="server" >
     <asp:TextBox runat="server" ID="txtWorkflowName" CssClass="textbox" 
             BorderColor="Lime" Width="562px" >Please enter workflow name</asp:TextBox>
         <br />
         <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
         <br />
      <asp:Button runat="server" ID="SaveWrkflw" CssClass="button" Text="Save Workflow" 
             onclick="SaveWrkflw_Click"/>
         <br />
         <asp:Panel ID="Panel" runat="server">
             <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
EmptyDataText="No Record Found" OnPageIndexChanging="GridView1_PageIndexChanging"
 PageSize="4" CellPadding="4"  
                 ForeColor="#333333" GridLines="None" >
                 <AlternatingRowStyle BackColor="White" />
                 <EditRowStyle BackColor="#2461BF" />
                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                 <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                 <RowStyle BackColor="#EFF3FB" />
                 <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             </asp:GridView>
         </asp:Panel>
     </asp:Panel>
      </div>
		
	<!-- content-wrap ends-->	
	</div>
	
	<!-- column starts --><!-- footer starts -->
	<div id="footer">		
			
		<p>
		&copy; 2011 <strong>SalesForce POC under creative user License</strong>
   	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>		

	<!-- footer ends -->		
  </div>	

<!-- wrap ends here -->
</div>

    </div>
    </form>
</body>
</html>
