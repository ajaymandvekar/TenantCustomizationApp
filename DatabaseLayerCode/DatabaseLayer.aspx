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
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatabaseLayer.aspx.cs" Inherits="Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SalesForce Home</title>
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
		
	<!-- content-wrap starts -->
	<div id="content-wrap">
	  <div id="main">
      <p style="font-size:2em;"><b>Please Select One of the Operations :</b></p>
      <br/>
          <asp:Button ID="BtnCreate" runat="server" Text="Create Object" class="button" onclick="BtnCreate_Click" Width=100%/>
          <br />
          <br />
          <br />
          <asp:Button ID="BtnDelete" runat="server" Text="Delete Object" class="button" onclick="BtnDelete_Click" Width=100%/>
          <br />
          <br />
          <br />
          <asp:Button ID="BtnInsert" runat="server" Text="Insert Data" class="button" onclick="BtnInsert_Click" Width=100%/>
          <br />
          <br />
          <br />
          <asp:Button ID="BtnModify" runat="server" Text="Modify Object" class="button" onclick="BtnModify_Click" Width=100%/>
          <br />
          <br />
          <br />
          <asp:Button ID="BtnViewObj" runat="server" Text="View Object" class="button" onclick="BtnViewObj_Click" Width=100%/>
          <br />
          <br />
          <br />
          <asp:Button ID="BtnViewData" runat="server" Text="View Data" class="button" onclick="BtnViewData_Click" Width=100%/>
          <br />
      <br/>

    	<!-- main ends -->	
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
