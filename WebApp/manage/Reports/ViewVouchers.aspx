<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ViewVouchers.aspx.cs" Inherits="WebApp.manage.Reports.ViewVouchers" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Vouchers Issued</h1>


	<asp:GridView ID="VouchersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="voucher_id">
		<Columns>
			<asp:BoundField DataField="voucher_id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="voucher_id" />
			<asp:HyperLinkField DataNavigateUrlFields="customer_id" ItemStyle-CssClass="action"
				DataTextField="customer_id"
				DataTextFormatString="{0}"
				DataNavigateUrlFormatString="/manage/Rewards/ViewCustomer.aspx?customer_id={0}"
				/>		
			<asp:BoundField DataField="code" HeaderText="Code" SortExpression="code" />
			<asp:BoundField DataField="dollar_value" HeaderText="Value" SortExpression="dollar_value" DataFormatString="{0:C}" />
			<asp:BoundField DataField="creation_datetime" HeaderText="Created" SortExpression="creation_datetime"  />
			
			<asp:BoundField DataField="expiry_datetime" HeaderText="Expires" SortExpression="expiry_datetime" />
			<asp:BoundField DataField="used_datetime" HeaderText="Used On" SortExpression="used_datetime" />
		</Columns>
		</asp:GridView>

<%--	<asp:SqlDataSource ID="VouchersSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>"  
		SelectCommand="select voucher_id , customer_id, code , dollar_value, creation_datetime, expiry_datetime , used_datetime FROM Vouchers	where company_id = @company_id">
		<SelectParameters>
			<asp:SessionParameter Name="company_id" SessionField="company_id" Type="Int32" />
		</SelectParameters>	
	</asp:SqlDataSource>--%>

</asp:Content>

