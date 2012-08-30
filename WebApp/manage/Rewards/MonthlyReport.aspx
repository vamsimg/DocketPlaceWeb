<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="True" CodeBehind="MonthlyReport.aspx.cs" Inherits="WebApp.manage.Rewards.MonthlyReport" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>Monthly Reports</h1>

     <span class="shortLeftLabel">Store: </span>  <asp:DropDownList ID="StoresDropDownList" runat="server" DataTextField="suburb" DataValueField="store_id" CssClass="textField" />               
     <div class="brclear"></div>    
     
     <span class="shortLeftLabel">Year/Month: </span>

     <asp:DropDownList ID="YearDropDownList" runat="server">
		<asp:ListItem Value="2012">2012</asp:ListItem>
		<asp:ListItem Value="2011">2011</asp:ListItem>		
	</asp:DropDownList>
		
	<asp:DropDownList ID="MonthDropDownList" runat="server">
		<asp:ListItem Value="1">January</asp:ListItem>
		<asp:ListItem Value="2">February</asp:ListItem>
		<asp:ListItem Value="3">March</asp:ListItem>
		<asp:ListItem Value="4">April</asp:ListItem>
		<asp:ListItem Value="5">May</asp:ListItem>
		<asp:ListItem Value="6">June</asp:ListItem>
		<asp:ListItem Value="7">July</asp:ListItem>
		<asp:ListItem Value="8">August</asp:ListItem>
		<asp:ListItem Value="9">September</asp:ListItem>
		<asp:ListItem Value="10">October</asp:ListItem>
		<asp:ListItem Value="11">November</asp:ListItem>
		<asp:ListItem Value="12">December</asp:ListItem>
	</asp:DropDownList>		
		
	<asp:Button ID="UpdateButton" runat="server" Text="Update" onclick="UpdateButton_Click" />		

	<div class="brclear"></div>	

	<br />

	<asp:GridView ID="SalesSummaryGridView" runat="server" AutoGenerateColumns="False" EnableViewState="false">
		<Columns>			
			<asp:BoundField DataField="type" HeaderText=""/>
			<asp:BoundField DataField="total_count" HeaderText="# of Sales" SortExpression="frequency" />
			<asp:BoundField DataField="total_revenue" HeaderText="Total Revenue" SortExpression="total_revenue"/>		
			<asp:BoundField DataField="average_sale" HeaderText="Average Sale"/>				
		</Columns>
	</asp:GridView>
	
	<br />

	<div class="brclear"></div>	

	<asp:GridView ID="RewardsSummaryGridView" runat="server" AutoGenerateColumns="False" EnableViewState="false">
		<Columns>			
			<asp:BoundField DataField="key" />
			<asp:BoundField DataField="value" HeaderText="Count"/>			
			<asp:BoundField DataField="dollar_value" HeaderText="$" HeaderStyle-HorizontalAlign="Center"/>			
		</Columns>
	</asp:GridView>	
	
	<br />

	<div class="brclear"></div>	

	<span class="leftLabel">New Members: </span>		
		<span class="textField"><asp:Literal ID="NewMemberCountLiteral" runat="server"  EnableViewState="false"></asp:Literal></span>

	<div class="brclear"></div> 				

	<span class="leftLabel">Members Visiting: </span>		
		<span class="textField"><asp:Literal ID="MonthlyMembersLiteral" runat="server"  EnableViewState="false"></asp:Literal></span>

	<div class="brclear"></div> 	

	<asp:Chart ID="DailyCountChart" runat="server">
		<Titles><asp:Title Text="Daily Number of Sales"></asp:Title></Titles>	
		<Series>
			<asp:Series Name="Series1">
		</asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="ChartArea1">
			<AxisX Title="Day"></AxisX>	
			<AxisY Title="Count" ></AxisY>
		</asp:ChartArea>
		</ChartAreas>
	</asp:Chart>	

	

	<asp:Chart ID="DailyRevenueChart" runat="server">
		<Titles><asp:Title Text="Daily Total Revenue"></asp:Title></Titles>	
		<Series>
			<asp:Series Name="Series1">
		</asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="ChartArea1">
			<AxisX Title="Day"></AxisX>	
			<AxisY Title="$" TextOrientation="Horizontal"></AxisY>
		</asp:ChartArea>
		</ChartAreas>
	</asp:Chart>	

	<br />
	<br />

	<asp:Chart ID="DailyAverageSalesChart" runat="server">
		<Titles><asp:Title Text="Daily Average Sale"></asp:Title></Titles>	
		<Series>
			<asp:Series Name="Series1">
		</asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="ChartArea1">
			<AxisX Title="Day"></AxisX>	
			<AxisY Title="$" TextOrientation="Horizontal"></AxisY>
		</asp:ChartArea>
		</ChartAreas>
	</asp:Chart>	

	<asp:Chart ID="NewMembersChart" runat="server">
		<Titles><asp:Title Text="Daily New Members"></asp:Title></Titles>	
		<Series>
			<asp:Series Name="Series1">
		</asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="ChartArea1">
			<AxisX Title="Day"></AxisX>	
			<AxisY Title="Count" ></AxisY>
		</asp:ChartArea>
		</ChartAreas>
	</asp:Chart>	

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

	
</asp:Content>

