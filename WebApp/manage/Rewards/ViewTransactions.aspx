<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ViewTransactions.aspx.cs" Inherits="WebApp.manage.Rewards.ViewTransactions" %>



<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


	<h1>Latest Sales</h1>
	

	<span class="leftLabel">Start Date:</span>
	
		<asp:TextBox ID="StartDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
			<cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="StartDateTextBox" Enabled="True"></cc1:CalendarExtender>
			<cc1:MaskedEditExtender ID="StartDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="StartDateTextBox"  CultureName="en-AU" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="$" CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" ></cc1:MaskedEditExtender>
	
	<div class="brclear"></div>
	
						
	<span class="leftLabel">End Date:</span>
		<asp:TextBox ID="EndDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
			<cc1:CalendarExtender ID="EndDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="EndDateTextBox" Enabled="True"></cc1:CalendarExtender>
			<cc1:MaskedEditExtender ID="EndDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="EndDateTextBox"  CultureName="en-AU" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="$" CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" ></cc1:MaskedEditExtender>
	
	<div class="brclear"></div> 

	<asp:Button ID="UpdateButton" runat="server" Text="Update" ValidationGroup="Update" onclick="UpdateButton_Click" />
		<span class="errorMessage"><asp:Literal ID="UpdateErrorLiteral" runat="server"  EnableViewState="false" ></asp:Literal></span>


	<div class="brclear"></div> 			

	<span class="leftLabel">Total # of Days:</span>
		<span class="textField"><asp:Literal ID="TotalDaysLiteral" runat="server"  EnableViewState="false"></asp:Literal></span>
	
	<div class="brclear"></div> 			

	<span class="leftLabel">Total # of Sales:</span>
		<span class="textField"><asp:Literal ID="SalesCountLiteral" runat="server"  EnableViewState="false" ></asp:Literal></span>
	
	<div class="brclear"></div> 				

	<span class="leftLabel">Total Revenue:</span>		
		<span class="textField"><asp:Literal ID="TotalRevenueLiteral" runat="server" EnableViewState="false"></asp:Literal></span>

	<div class="brclear"></div> 			

	<span class="leftLabel">Average Daily Sale Count:</span>		
		<span class="textField"><asp:Literal ID="AverageDailySaleCountLiteral" runat="server"  EnableViewState="false"></asp:Literal></span>

	<div class="brclear"></div> 			

	<span class="leftLabel">Average Daily Revenue:</span>		
		<span class="textField"><asp:Literal ID="AverageDailyRevenueLiteral" runat="server"  EnableViewState="false" ></asp:Literal></span>

	<div class="brclear"></div> 			

	<span class="leftLabel">Average Sale:</span>		
		<span class="textField"><asp:Literal ID="AverageSaleLiteral" runat="server"  EnableViewState="false"></asp:Literal></span>

	<div class="brclear"></div> 			
	
	<asp:GridView ID="DocketsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="docket_id" EnableViewState="false">
		<Columns>
			<asp:HyperLinkField DataNavigateUrlFields="docket_id" ItemStyle-CssClass="action"
				DataTextField="docket_id"
				DataTextFormatString ="{0:G}"
				DataNavigateUrlFormatString="/manage/Rewards/ViewReceipt.aspx?docket_id={0}"
				Text="{0}"/>		
			<asp:BoundField DataField="local_id" HeaderText="Sale ID" SortExpression="local_id" />			
			<asp:HyperLinkField DataNavigateUrlFields="customer_id" ItemStyle-CssClass="action"
				DataTextField="customer_id"
				DataTextFormatString="{0}"
				DataNavigateUrlFormatString="/manage/Rewards/ViewCustomer.aspx?customer_id={0}"
				/>		
			<asp:BoundField DataField="store_id" HeaderText="Store" SortExpression="store_id" />
			<asp:BoundField DataField="total" HeaderText="Total" SortExpression="total" />
			<asp:BoundField DataField="reward_points" HeaderText="Rewards" SortExpression="reward_points" />			
			<asp:BoundField DataField="creation_datetime" HeaderText="Created On" SortExpression="creation_datetime"/>
		</Columns>
	</asp:GridView>

	
</asp:Content>

