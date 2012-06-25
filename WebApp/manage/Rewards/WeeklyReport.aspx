<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="WeeklyReport.aspx.cs" Inherits="WebApp.manage.Rewards.WeeklyReport" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>Weekly Report</h1>

     <span class="shortLeftLabel">Store: </span>  <asp:DropDownList ID="StoresDropDownList" runat="server" DataTextField="suburb" DataValueField="store_id" CssClass="textField" />               
     <div class="brclear"></div>    

	<span class="shortLeftLabel">Ending:</span>
	
		<asp:TextBox ID="DailyDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
			<cc1:CalendarExtender ID="DailyDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="DailyDateTextBox" Enabled="True"></cc1:CalendarExtender>
			<cc1:MaskedEditExtender ID="DailyDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="DailyDateTextBox"  CultureName="en-AU" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="$" CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" ></cc1:MaskedEditExtender>
	
		<asp:Button ID="UpdateButton" runat="server" Text="Update" onclick="UpdateButton_Click" />

	<div class="brclear"></div>	

	<asp:GridView ID="SalesSummaryGridView" runat="server" AutoGenerateColumns="False" EnableViewState="false">
		<Columns>			
			<asp:BoundField DataField="type" HeaderText=""/>
			<asp:BoundField DataField="total_count" HeaderText="# of Sales" SortExpression="frequency" />
			<asp:BoundField DataField="total_revenue" HeaderText="Total Revenue" SortExpression="total_revenue"/>		
			<asp:BoundField DataField="average_sale" HeaderText="Average Sale"/>				
		</Columns>
	</asp:GridView>

	<br />

	<asp:Chart ID="DailyCountChart" runat="server">
		<Titles><asp:Title Text="Daily Number of Sales"></asp:Title></Titles>	
		<Series>
			<asp:Series Name="Series1">
		</asp:Series>
		</Series>

		<ChartAreas>
			<asp:ChartArea Name="ChartArea1">
			<AxisX Title="Day"></AxisX>	
			<AxisY Title="Count"></AxisY>
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
			<asp:BoundField DataField="unit_cost" HeaderText="Unit Cost" DataFormatString="{0:C0}" />	
			<asp:BoundField DataField="total_count" HeaderText="# of Sales" SortExpression="frequency" />
			<asp:BoundField DataField="total_revenue" HeaderText="Total Revenue" SortExpression="total_revenue" DataFormatString="{0:C0}" />		
			<asp:BoundField DataField="description" HeaderText="Description"/>
		</Columns>
	</asp:GridView>	

	
</asp:Content>

