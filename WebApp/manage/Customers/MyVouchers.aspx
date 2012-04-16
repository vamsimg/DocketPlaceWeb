<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="MyVouchers.aspx.cs" Inherits="WebApp.manage.Customers.MyVouchers" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>My Vouchers</h1>

	<asp:GridView ID="VouchersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="voucher_id" DataSourceID="VouchersSqlDataSource">
		<Columns>
			<asp:BoundField DataField="name" HeaderText="Store" SortExpression="name" />						
			<asp:BoundField DataField="dollar_value" HeaderText="Value" SortExpression="dollar_value" DataFormatString="{0:C}" />
			<asp:BoundField DataField="creation_datetime" HeaderText="Created" SortExpression="creation_datetime"  DataFormatString="{0:dd-MMM-yyyy}"  />
			<asp:BoundField DataField="expiry_datetime" HeaderText="Expires" SortExpression="expiry_datetime"  DataFormatString="{0:dd-MMM-yyyy}" />			
			<asp:BoundField DataField="used_datetime" HeaderText="Used On" SortExpression="used_datetime"  DataFormatString="{0:dd-MMM-yyyy}" />			
		</Columns>
		</asp:GridView>

	<asp:SqlDataSource ID="VouchersSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>"  
		SelectCommand="select voucher_id, c.name, v.dollar_value, v.creation_datetime, v.expiry_datetime , v.used_datetime 
					FROM Vouchers as v
					INNER JOIN
					Companies as c
					ON v.company_id = c.company_id
					where customer_id = @customer_id">
		<SelectParameters>
			<asp:SessionParameter Name="customer_id" SessionField="customer_id" Type="Int32" />
		</SelectParameters>	
	</asp:SqlDataSource>

</asp:Content>

