<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="MyReceipts.aspx.cs" Inherits="WebApp.manage.Customers.MyReceipts" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	
	<h1>My Receipts</h1>

	<asp:GridView ID="ReceiptsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="docket_id" DataSourceID="ReceiptsSqlDataSource">
		<Columns>
			<asp:BoundField DataField="creation_datetime" HeaderText="Date" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}"  />
			<asp:BoundField DataField="name" HeaderText="Store" SortExpression="name" />
			<asp:BoundField DataField="suburb" HeaderText="Suburb" SortExpression="suburb" />
			<asp:BoundField DataField="total" HeaderText="Total" SortExpression="total" DataFormatString="{0:C}"  />
			<asp:BoundField DataField="reward_points" HeaderText="Rewards" SortExpression="reward_points" />
			<asp:HyperLinkField DataNavigateUrlFields="docket_id"  Text="View" DataNavigateUrlFormatString="/manage/Customers/ViewReceipt.aspx?docket_id={0}" Target="_blank" HeaderText="View" />			
		</Columns>
	</asp:GridView>
	
	<asp:SqlDataSource ID="ReceiptsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" 
		SelectCommand="SELECT docket_id, total, reward_points, d.creation_datetime, c.name, s.suburb 
		FROM Dockets as d
		INNER JOIN Stores as s
		ON d.store_id = s.store_id
		INNER JOIN Companies as c
		ON s.company_id = c.company_id
		WHERE d.customer_id = @customer_id">		
		<SelectParameters>
			<asp:SessionParameter Name="customer_id" SessionField="customer_id" Type="Int32" />
		</SelectParameters>
		
	</asp:SqlDataSource>
	
</asp:Content>

