<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="CreateInvoice.aspx.cs" Inherits="WebApp.admin.CreateInvoice" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


	<asp:DropDownList ID="CompaniesDropDownList" runat="server" DataSourceID="CompaniesSqlDataSource" DataTextField="name" DataValueField="company_id" onselectedindexchanged="CompaniesDropDownList_SelectedIndexChanged" AutoPostBack="True">
	</asp:DropDownList>	

	<asp:SqlDataSource ID="CompaniesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" SelectCommand="SELECT [company_id], [name] FROM [Companies]"></asp:SqlDataSource>

	<h2>Billing Items</h2>

	<span class="leftLabel">Description</span>	<asp:TextBox ID="DescriptionTextBox" runat="server" MaxLength="1000" Columns="100"></asp:TextBox>

	<div class="brclear"></div>
	
	<span class="leftLabel">Quantity:</span>		<asp:TextBox ID="QuantityTextBox" runat="server"></asp:TextBox>	

	<div class="brclear"></div>		

	<span class="leftLabel">Unit cost:</span>	$<asp:TextBox ID="UnitCostTextBox" runat="server" Columns="4"></asp:TextBox>

	<ajaxToolkit:MaskedEditExtender ID="UnitCostTextBox_MaskedEditExtender" runat="server" MaskType="Number" Mask="999.99" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" TargetControlID="UnitCostTextBox">
	</ajaxToolkit:MaskedEditExtender>

	<div class="brclear"></div>	

	<span class="leftLabel">Is Credit ?</span> 	<asp:CheckBox ID="IsCreditCheckBox" runat="server" />

	<div class="brclear"></div>

	<asp:Button ID="CreateItemButton" runat="server" Text="Create" onclick="CreateItemButton_Click" /> <span class="errorMessage"></span>

	<br />
	<br />

	<asp:GridView ID="BillingItemsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="billingitem_id" DataSourceID="BillingItemsSqlDataSource">
		<Columns>			
			<asp:BoundField DataField="billingitem_id" HeaderText="Item ID" />
			<asp:BoundField DataField="description" HeaderText="Description" />
			<asp:BoundField DataField="quantity" HeaderText="Quantity" />			
			<asp:BoundField DataField="unit_cost" HeaderText="Unit Cost" DataFormatString="{0:C}" />
			<asp:BoundField DataField="total_amount" HeaderText="Total" DataFormatString="{0:C}" />
			<asp:BoundField DataField="creation_datetime" HeaderText="Created On" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}" />			
		</Columns>
	</asp:GridView>

	<asp:SqlDataSource ID="BillingItemsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" 
			SelectCommand="select billingitem_id, description, quantity, unit_cost, total_amount, creation_datetime  
			FROM BillingItems as b
			where b.company_id = @company_id and invoice_id IS NULL">
		<SelectParameters>
			<asp:ControlParameter ControlID="CompaniesDropDownList" Name="company_id" PropertyName="SelectedValue" />
		</SelectParameters>
	</asp:SqlDataSource>	

	<h2>Invoices</h2>

	<span class="leftLabel">Start Date:</span>
			<asp:TextBox ID="StartDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
			<cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="StartDateTextBox"></cc1:CalendarExtender>
			<cc1:MaskedEditExtender ID="StartDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="StartDateTextBox"></cc1:MaskedEditExtender>
			<asp:RequiredFieldValidator ID="StartDateRequiredFieldValidator" runat="server" ErrorMessage="Start Date Required" Display="dynamic" ControlToValidate="StartDateTextBox" ValidationGroup="NewInvoice"></asp:RequiredFieldValidator>
	          
	<div class="brclear"></div> 

	<span class="leftLabel">End Date:</span>
			<asp:TextBox ID="EndDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
			<cc1:CalendarExtender ID="EndDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="EndDateTextBox"></cc1:CalendarExtender>
			<cc1:MaskedEditExtender ID="EndDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="EndDateTextBox"></cc1:MaskedEditExtender>
			<asp:RequiredFieldValidator ID="EndDateRequiredFieldValidator" runat="server" ErrorMessage="End Date Required" Display="dynamic" ControlToValidate="EndDateTextBox" ValidationGroup="NewCampaign" ></asp:RequiredFieldValidator>
			<asp:CompareValidator ID="DateCompareValidator" runat="server" ErrorMessage="End date must be later than start date." Display="Dynamic" ControlToCompare="StartDateTextBox" ControlToValidate="EndDateTextBox" Operator="GreaterThan" Type="Date" ValidationGroup="NewInvoice"></asp:CompareValidator>
	<div class="brclear"></div> 

	
	<span class="leftLabel">Notes:</span> <asp:TextBox ID="NotesTextBox" runat="server" Columns="100"></asp:TextBox>			
			
	<div class="brclear"></div> 

	<asp:Button ID="CreateInvoiceButton" runat="server" Text="Create Invoice" onclick="CreateInvoiceButton_Click" ValidationGroup="NewInvoice" />

	<br />
	<br />

	<asp:GridView ID="InvoicesGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="invoice_id" DataSourceID="InvoicesSqlDataSource">
		<Columns>			
			<asp:HyperLinkField DataNavigateUrlFields="invoice_id" Target="_blank"
						DataTextFormatString ="{0:G}"
						DataNavigateUrlFormatString="/manage/Billing/ViewInvoice.aspx?invoice_id={0}"
						Text="View">			
			<ItemStyle CssClass="action" />
			</asp:HyperLinkField>
			<asp:BoundField DataField="invoice_id" HeaderText="Invoice ID" />
			<asp:BoundField DataField="total_amount" HeaderText="Total" DataFormatString="{0:C}" />
			<asp:BoundField DataField="creation_datetime" HeaderText="Created On" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}" />			
			<asp:BoundField DataField="start_datetime" HeaderText="Start" SortExpression="start_datetime" DataFormatString="{0:dd-MMM-yyyy}" />			
			<asp:BoundField DataField="end_datetime" HeaderText="Created On" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}" />			
			<asp:BoundField DataField="paid_datetime" HeaderText="Created On" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}" />			
			
		</Columns>
	</asp:GridView>

	<asp:SqlDataSource ID="InvoicesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" 
			SelectCommand="select invoice_id, total_amount, creation_datetime, start_datetime, end_datetime, creation_datetime, paid_datetime, is_credit    
			FROM Invoices as i
			where i.company_id = @company_id">
		<SelectParameters>
			<asp:ControlParameter ControlID="CompaniesDropDownList" Name="company_id" PropertyName="SelectedValue" />
		</SelectParameters>
	</asp:SqlDataSource>	



	

</asp:Content>

