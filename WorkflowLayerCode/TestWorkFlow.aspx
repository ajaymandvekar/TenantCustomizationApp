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
<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="TestWorkFlow.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Testing Workflow</title>
    <link rel="stylesheet" href="../images/HarvestField.css" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-size: medium;
            font-weight: bold;
        }
        .style2
        {
            text-decoration: underline;
        }
    </style>
</head>
<body>
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
      <form id="form1" runat="server">
      </form>
      </div>
      </div>
          
    </div>	
    </body>
</html>

