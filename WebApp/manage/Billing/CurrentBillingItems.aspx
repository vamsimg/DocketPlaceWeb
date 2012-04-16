<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="CurrentBillingItems.aspx.cs" Inherits="WebApp.manage.Billing.CurrentBillingItems" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	
	<h1>Billing</h1>

	<h2>Current Items</h2>
	
	<i>An invoice will be generated for these items at the beginning of the month</i>

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
			<asp:SessionParameter Name="company_id" SessionField="company_id" Type="Int32" />
		</SelectParameters>
	</asp:SqlDataSource>	
	
	<h1>Invoices</h1>

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
			<asp:BoundField DataField="end_datetime" HeaderText="End" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}" />			
			<asp:BoundField DataField="paid_datetime" HeaderText="Paid" SortExpression="paid_datetime" DataFormatString="{0:dd-MMM-yyyy}" />			
			
		</Columns>
	</asp:GridView>

	<asp:SqlDataSource ID="InvoicesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" 
			SelectCommand="select invoice_id, total_amount, creation_datetime, start_datetime, end_datetime, creation_datetime, paid_datetime, is_credit    
			FROM Invoices as i
			where i.company_id = @company_id">
		<SelectParameters>
			<asp:SessionParameter Name="company_id" SessionField="company_id" Type="Int32" />
		</SelectParameters>
	</asp:SqlDataSource>	
	

</asp:Content>


