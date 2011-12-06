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
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkflowHome.aspx.cs" Inherits="Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SalesForce Home</title>
    <link rel="stylesheet" href="../images/HarvestField.css" type="text/css" />
    <style type="text/css">
        .style2
        {
            font-family: Arial;
        }
        .style3
        {
            text-align: center;
        }
        .style4
        {
            text-decoration: underline;
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
	  <div id="main">
      <h2 style="font-size:2em;" class="style3"><b><span class="style2"><em>Workflow 
          Customization Layer:</em></span><br />
          </b></h2>
          <p style="font-size:2em;">&nbsp;</p>
          <h4><strong>Please select the operation to perform:</strong></h4>
      <br/>
          
    	<!-- main ends -->	
          <asp:Button ID="BtnCreateWebService" runat="server" Text="Add new service" 
              class="button"  Width=100% onclick="BtnCreateWebService_Click" 
              CssClass="style4" Font-Names="Arial Black" Font-Size="Small" 
              BackColor="#33CC33"/>
          <br />
          <br />
          <asp:Button ID="BtnViewWrkflw" runat="server" Text="View Existing Workflows" 
              class="button" onclick="BtnViewWrkflw_Click" Width=100% CssClass="style4" 
              Font-Names="Arial Black" Font-Size="Small" BackColor="#33CC33"/>
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

    </form>
</body>
</html>
