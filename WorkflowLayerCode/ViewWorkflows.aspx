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
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewWorkflows.aspx.cs" Inherits="ViewWorkflows" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SalesForce Home</title>
    <link rel="stylesheet" href="../images/HarvestField.css" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-size: medium;
            font-weight: bold;
            text-align: center;
            height: 6px;
            width: 333px;
        }
        .style2
        {
            text-decoration: underline;
            color: #33CC33;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
		
	<!-- content-wrap ends-->	
	  <div id="main">
      <p style="font-size:2em;"><b><span class="style2"><em>Welcome to workflow customization:</em></span><br />
          </b></p>
          <p class="style1">Please select one of the Workflows :</p>
          <asp:Table ID="WfTable" runat="server" BorderStyle="Solid" BorderWidth="2px" 
              CellPadding="3" CellSpacing="3" Font-Bold="True" Font-Size="Medium" 
              GridLines="Both" HorizontalAlign="Center">
          </asp:Table>
      <br/>
          <asp:Button ID="Button1" runat="server" Text="Customize Workflow" 
              onclick="Button1_Click" BackColor="#33CC33" BorderColor="Black" 
              BorderStyle="Solid" BorderWidth="0.2em" Font-Bold="True" 
              Font-Names="Copperplate Gothic Bold" Font-Size="Medium" Width="551px" /><br /><br />

              <asp:Button ID="Button2" runat="server" Text="Test Workflow" 
               BackColor="#33CC33" BorderColor="Black" 
              BorderStyle="Solid" BorderWidth="0.2em" Font-Bold="True" 
              Font-Names="Copperplate Gothic Bold" Font-Size="Medium" Width="551px" 
              onclick="Button2_Click" />

          <br />
          <br />
	  </div>
		
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

    </form>
</body>
</html>
