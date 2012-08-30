﻿<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="True" CodeBehind="YearlyReport.aspx.cs" Inherits="WebApp.manage.Rewards.YearlyReport" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>Yearly Report</h1>
     	
     <span class="shortLeftLabel">Store: </span>  <asp:DropDownList ID="StoresDropDownList" runat="server" DataTextField="suburb" DataValueField="store_id" CssClass="textField" />               
     <div class="brclear"></div>    
     
     <span class="shortLeftLabel">Year: </span>     
     
	<asp:DropDownList ID="YearDropDownList" runat="server">
		<asp:ListItem Value="2011">2011</asp:ListItem>
		<asp:ListItem Value="2012">2012</asp:ListItem>
	</asp:DropDownList>

	<asp:Button ID="UpdateButton" runat="server" Text="Update" onclick="UpdateButton_Click" />
		
	<div class="brclear"></div>	

	<span class="leftLabel">Total # of Sales:</span>
		<span class="textField"><asp:Literal ID="SalesCountLiteral" runat="server"  EnableViewState="false" ></asp:Literal></span>
	
	<div class="brclear"></div> 				

	<span class="leftLabel">Total Revenue:</span>		
		<span class="textField"><asp:Literal ID="TotalRevenueLiteral" runat="server" EnableViewState="false"></asp:Literal></span>

	<div class="brclear"></div> 			
	
	<span class="leftLabel">Average Sale:</span>		
		<span class="textField"><asp:Literal ID="AverageSaleLiteral" runat="server"  EnableViewState="false"></asp:Literal></span>

	<div class="brclear"></div> 		


	<br />

	<asp:Chart ID="MonthlyCountChart" runat="server">
		<Titles><asp:Title Text="Monthly Number of Sales"></asp:Title></Titles>	
		<Series>
			<asp:Series Name="Series1">
		</asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="ChartArea1">
			<AxisX Title="Month"></AxisX>	
			<AxisY Title="Count"></AxisY>
		</asp:ChartArea>
		</ChartAreas>
	</asp:Chart>	

	

	<asp:Chart ID="MonthlyRevenueChart" runat="server">
		<Titles><asp:Title Text="Monthly Total Revenue"></asp:Title></Titles>	
		<Series>
			<asp:Series Name="Series1">
		</asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="ChartArea1">
			<AxisX Title="Month"></AxisX>	
			<AxisY Title="$" TextOrientation="Horizontal"></AxisY>
		</asp:ChartArea>
		</ChartAreas>
	</asp:Chart>	



	<asp:Chart ID="MonthlyAverageSalesChart" runat="server">
		<Titles><asp:Title Text="Monthly Average Sale"></asp:Title></Titles>	
		<Series>
			<asp:Series Name="Series1">
		</asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="ChartArea1">
			<AxisX Title="Month"></AxisX>	
			<AxisY Title="$" TextOrientation="Horizontal"></AxisY>
		</asp:ChartArea>
		</ChartAreas>
	</asp:Chart>	

	<h3>Products Sold</h3>

	<asp:GridView ID="DocketItemsGridView" runat="server" AutoGenerateColumns="False" EnableViewState="false">
		<Columns>			
			<asp:BoundField DataField="product_code" HeaderText="Product Code"/>
			<asp:BoundField DataField="product_barcode" HeaderText="Product Barcode"/>
               <asp:BoundField DataField="department" HeaderText="Department"/>
               <asp:BoundField DataField="category" HeaderText="Category"/>
			<asp:BoundField DataField="cost_ex" HeaderText="Cost Ex" DataFormatString="{0:C0}" />	
               <asp:BoundField DataField="sale_ex" HeaderText="Sale Ex" DataFormatString="{0:C0}" />	
               <asp:BoundField DataField="sale_inc" HeaderText="Sale Inc" DataFormatString="{0:C0}" />	
			<asp:BoundField DataField="total_count" HeaderText="# of Sales" SortExpression="frequency" />
			<asp:BoundField DataField="total_revenue" HeaderText="Total Revenue" SortExpression="total_revenue" DataFormatString="{0:C0}" />		
			<asp:BoundField DataField="description" HeaderText="Description"/>
		</Columns>
	</asp:GridView>	

	<h3>Visiting Customers</h3>

	<asp:GridView ID="CustomersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="customer_id" EnableViewState="false">
		<Columns>						
			<asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />
			<asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name" />
			<asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name" />			
			<asp:BoundField DataField="suburb" HeaderText="Suburb" SortExpression="suburb" />
			<asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="mobile" />
			<asp:BoundField DataField="total_revenue" HeaderText="Total Revenue" SortExpression="total_revenue" DataFormatString="{0:C0}" />
			<asp:BoundField DataField="frequency" HeaderText="# of Sales" SortExpression="frequency" />			
			<asp:HyperLinkField DataNavigateUrlFields="customer_id" ItemStyle-CssClass="action" HeaderText="DocketPlace ID"
				DataTextFormatString ="{0:G}"
				DataTextField="customer_id"
				DataNavigateUrlFormatString="/manage/Rewards/ViewCustomer.aspx?customer_id={0}"
				Text="{0}"/>		
		</Columns>
	</asp:GridView>

</asp:Content>

