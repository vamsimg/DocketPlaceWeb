<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ViewInvoice.aspx.cs" Inherits="WebApp.manage.Billing.ViewInvoice" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<asp:HyperLink ID="PrintHyperLink" runat="server">Print</asp:HyperLink>

	<h1>Invoice # <asp:Literal ID="InvoiceIDLiteral" runat="server"></asp:Literal></h1> 

	<h2>DocketPlace Pty Ltd</h2>

	<h3>ABN: 35152155550</h3>	

	<h3>Contact: Vamsi Gullapalli 0432100092</h3>

	<span class="leftLabel">Customer: </span><asp:Literal ID="CustomerLiteral" runat="server"></asp:Literal>

	<div class="brclear"></div>	

	<span class="leftLabel">Invoice Date:</span><asp:Literal ID="InvoiceDateLiteral" runat="server"></asp:Literal>

	<div class="brclear"></div>		

	<span class="leftLabel">Billing Period:</span><asp:Literal ID="BillingPeriodLiteral" runat="server"></asp:Literal>

	<div class="brclear"></div>		
	
	<span class="leftLabel">Paid On:</span><asp:Literal ID="PaymentDateLiteral" runat="server"></asp:Literal>

	<div class="brclear"></div>	

	<span class="leftLabel">Terms:</span><asp:Literal ID="TermsLiteral" runat="server"></asp:Literal>
		
	<div class="brclear"></div>	
	
	<span class="leftLabel">Bank Details:</span>
	
	<div class="brclear"></div>			

	<i>BSB: </i>062021<br />
	<i>Account Number: </i> 10520838<br />
	<i>Account Name:  </i>DocketPlace P/L<br />

	<p>Please include the invoice number and your business name in the description field when making payment.</p>

	<asp:GridView ID="BillingItemsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="billingitem_id" DataSourceID="BillingItemsSqlDataSource">
		<Columns>			
			<asp:BoundField DataField="creation_datetime" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" />			
			<asp:BoundField DataField="billingitem_id" HeaderText="Item ID" />
			<asp:BoundField DataField="description" HeaderText="Description" />
			<asp:BoundField DataField="quantity" HeaderText="Quantity" />			
			<asp:BoundField DataField="unit_cost" HeaderText="Unit Cost" DataFormatString="{0:C}" />
			<asp:BoundField DataField="total_amount" HeaderText="Total" DataFormatString="{0:C}" />			
		</Columns>
	</asp:GridView>

	<asp:SqlDataSource ID="BillingItemsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" 
			SelectCommand="select billingitem_id, description, quantity, unit_cost, total_amount, creation_datetime  
			FROM BillingItems as b
			where b.invoice_id = @invoice_id">
		<SelectParameters>
			<asp:QueryStringParameter Name="invoice_id" QueryStringField="invoice_id" />
		</SelectParameters>
	</asp:SqlDataSource>	

	<br />
	<br />
	
	<span class="leftLabel">Sub Total:</span><asp:Literal ID="SubtotalLiteral" runat="server"></asp:Literal>
	
	<div class="brclear"></div>	
	
	<span class="leftLabel">10% GST:</span><asp:Literal ID="GSTLiteral" runat="server"></asp:Literal>	

	<div class="brclear"></div>		

	<span class="leftLabel">Total:</span><asp:Literal ID="TotalLiteral" runat="server"></asp:Literal>	

</asp:Content>

